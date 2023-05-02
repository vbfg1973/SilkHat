# SilkHat

[![.NET](https://github.com/vbfg1973/CodeAnalysis/actions/workflows/dotnet.yml/badge.svg)](https://github.com/vbfg1973/CodeAnalysis/actions/workflows/dotnet.yml)

## Introduction

The purpose of this project is to identify and prioritise technical debt. It mainly does so by investigating a git
repository and identifying those files, classes and methods / functions which are:

1) Complex
2) Subject to change

In general, safe changes happen when code is well understood and when code is architected to accomodate change.

Code that changes a lot over time can be considered a *code smell*. Complex code that changes a lot over time is very
smelly indeed.

This project seeks to identify code that is both subject to change and complex in order to prioritise **hotspots**
within the code that arte likely to be the causes of failure, friction and the ties that bind. Code that is complex and
has a large number of people working on it is particularly fraught with these issues and this in turn is further
identified.

Intended features:

1) Identify files, classes, and methods which are particularly subject to change
   1) Plot these methods against complexity and number of people working on them
   2) Plot complexity and rate of change over time
2) Identify possible architectural smells through the detection of code that changes together. 
3) Identify related code and possible modules / bounded contexts in a *big ball of mud* through identification of:
   1) unnoticed teams (groups of people commonly working on the same areas of the code base)
   2) unnoticed modules (groups of code that change together or within windows of time)

## About the name

> One of the low on whom assurance sits
>
> As a silk hat on a Bradford millionaire.
>
> - T.S.Eliot

1) I am low
2) I am from Bradford
3) The project is intended to provide some degree of assurance
    - Through the prioritisation of tech debt identification
    - Work on the right things