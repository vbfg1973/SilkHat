# Git Infrastructure

I originally tried building this with libgit2sharp but it is painfully slow, even for relatively trivial operations such
as cycling through commits. On my machine it takes roughly 4 hours to cycle through all commits on the master branch of
the linux kernel project. Shelling the work to the git command on the command line and redirecting to a file will take a
little over a minute. So that's the general approach used here.

## General Architecture

There is a one to one relationship between a type of operation and a command object. An implementation of
IProcessCommandRunner is passed in to each command object. This does the real work of shelling the process and returning
the STDOUT lines as an *IEnumerable\<string\>* from that process to the calling command object.

It is then the responsibility of the parent command object to parse those lines into meaningful objects that can be
returned to the ultimate caller of the process.

This approach allows for:

* A mocked IProcessCommandRunner to lift representative output off the disk in test libraries so that the parsing can be
  properly tested without relying on actual git repositories.
* Anything you can do on the command line you can do here provided you can figure out how to parse the output

## Command Lines

To ensure the actual command lines are consistent the command line arguments are defined as private records within the
command object itself. Example from CommitDetails:

```
        private record CommitDetailsGitCommandLineArguments : AbstractGitCommandLineArguments
        {
            public CommitDetailsGitCommandLineArguments(string path)
            {
                Arguments = new List<string>
                {
                    Path.Combine($"--git-dir={path}", ".git"),
                    $"--work-tree={path}",
                    "log",
                    "--name-status"
                }.ToImmutableList();
            }
        }
```

AbstractGitCommandLineArgument contains the *git* command. The arguments listed here are combined into one
space-separated string. They are ImmutableLists so cannot be altered except pre-compilation.

## Commands Used

### GitCommitDetails

Retrieve as many details about commits as possible, including message body and files modified with their ChangeKind (
Add, Delete, Modify, etc). Return all commits on the current branch.

```
    git --git-dir=$PATH/.git --work-tree=$PATH log --name-status
```

### GitCommitParents

Retrieve the full list of parents from the specified commit

```
    git --git-dir=$PATH/.git --work-tree=$PATH rev-parse $SHA^@
```