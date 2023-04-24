``` ini

BenchmarkDotNet=v0.13.5, OS=Windows 10 (10.0.19045.2846/22H2/2022Update)
AMD Ryzen 9 3950X, 1 CPU, 32 logical and 16 physical cores
.NET SDK=7.0.203
  [Host]     : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.5 (7.0.523.17405), X64 RyuJIT AVX2


```
|                     Method |      Mean |     Error |    StdDev | Ratio |   Gen0 | Allocated | Alloc Ratio |
|--------------------------- |----------:|----------:|----------:|------:|-------:|----------:|------------:|
|      LeadingWhiteSpaceLinq | 11.558 μs | 0.1865 μs | 0.1744 μs |  1.00 | 0.2594 |    2288 B |        1.00 |
| LeadingWhitespaceIteration |  1.911 μs | 0.0276 μs | 0.0258 μs |  0.17 |      - |         - |        0.00 |
