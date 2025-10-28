# COSC_335_MemoryManagementProject

Memory Management Demo (C#)

This is a small, educational console project that demonstrates several core memory-management concepts in C#:

- Stack behavior (LIFO, call-stack growth via recursion)
- Heap usage (objects, references, and garbage-collection eligibility)
- Buffering and streaming (reading data in chunks from memory and files)

The code is intentionally simple and verbose so it can be used for classroom demos or self-study.

Repository layout (important files)
---------------------------------

- `COSC_335_MemoryManagementProject/COSC_335_MemoryManagementProject.csproj` — project file
- `Project Files (.cs)/Program.cs` — entry point; runs the demos in sequence
- `Project Files (.cs)/StackExample.cs` — stack (LIFO) demo and a recursive method that illustrates call-stack growth
- `Project Files (.cs)/HeapExample.cs` — heap demo using `Table` and `Order` objects
- `Project Files (.cs)/BufferExample.cs` — in-memory buffer demo (reads a long string through a MemoryStream in fixed-size chunks)
- `Project Files (.cs)/BufferExample2.cs` — file buffering demo; reads `projecttext.txt` using FileStream, BufferedStream and StreamReader with a robust lookup for the file
- `projecttext.txt` — explanatory file read by `BufferExample2`

What the program does
---------------------

When run, the program prints descriptive headings and runs four demos:

1. Stack demo — uses a `Stack<string>` to show LIFO push/pop behavior and a recursive helper `CauseStackOverflow` to illustrate call-stack growth.
2. Heap demo — creates `Table` and `Order` objects, assigns and clears references to show when objects become eligible for garbage collection.
3. Buffer demo (in-memory) — converts a long string to bytes and reads it in 10-byte chunks using a `MemoryStream` and a small byte[] buffer.
4. Buffer demo 2 (file) — reads `projecttext.txt` line-by-line using `FileStream` -> `BufferedStream` -> `StreamReader` and prints each line with its number.

Key implementation notes
------------------------

- StackExample:
  - Demonstrates push/pop (LIFO) using `Stack<T>` and prints the steps.
  - Contains `CauseStackOverflow(int level)` to show how recursion expands the call stack; note that in real .NET a `StackOverflowException` is typically fatal and cannot reliably be caught.

- HeapExample:
  - Uses `Table` and `Order` classes to show objects allocated on the heap and how clearing references (e.g., `table1 = null`) makes objects eligible for garbage collection.

- BufferExample:
  - Shows chunked reads from a `MemoryStream`. This demonstrates handling data in small buffers to reduce memory pressure and avoid large single allocations.

- BufferExample2 (important change)
  - To avoid requiring `projecttext.txt` to live in the `bin` output folder, `BufferExample2` now searches for `projecttext.txt` by walking up parent directories from the current working directory and from the application base directory (the latter covers cases when the app runs from `bin`).
  - The search depth is limited (the sample code searches up to 6 parent levels). This makes the demo resilient when you run the app from the project root or from the build output without hard-coded absolute paths.

Example console output (representative)
-------------------------------------

```
=== STACK DEMO ===
Imagine a stack of plates — Last In, First Out (LIFO).

Adding plates to the stack...
Pushed Plate #1
Pushed Plate #2
Pushed Plate #3
Pushed Plate #4
Pushed Plate #5

Now removing plates from the stack (LIFO order):
Popped Plate #5
Popped Plate #4
Popped Plate #3
Popped Plate #2
Popped Plate #1

=== HEAP DEMO ===
Restaurant Memory Management Demo:

Table 1 (Window):
  Main Course: Pasta
  Dessert: Tiramisu

Table 2 (Patio):
  Main Course: Steak
  Dessert: Cheesecake

=== BUFFER DEMO - In Program/String ===
Buffer #1: This is a
Buffer #2:  string of
... (etc)

=== BUFFER DEMO 2 - In .txt file ===
Reading data from 'projecttext.txt' using a buffer:

Line 1: Welcome to the Memory Management Demo!
Line 2: This file is being read using FileStream, BufferedStream, and StreamReader.
... (rest of file lines)
```

How to build and run (Windows PowerShell)
---------------------------------------

Open PowerShell in the repository root and run:

```powershell
dotnet build
dotnet run --project "COSC_335_MemoryManagementProject/COSC_335_MemoryManagementProject.csproj"
```

If `BufferExample2` can't find `projecttext.txt`, it will print a helpful message indicating where it searched. If you prefer to always use a fixed relative path, you can update `BufferExample2` to compute a path relative to the repository root or include `projecttext.txt` as a content file in the project file.

Notes about StackOverflowException and GC
----------------------------------------

- `StackOverflowException` in .NET is typically fatal and the runtime will often terminate the process; the sample recursion is only for demonstration and should not be used to provoke a real overflow.
- The demos show when objects become eligible for garbage collection; they do not force collection. Use `dotnet-counters`, `dotnet-trace` or Visual Studio Diagnostic Tools if you want to observe GC behavior at runtime.

Authorship:
------
GitHub Copilot Chat was used to assist in the understanding of code, as well as fixing errors, and updating the README.md file.

Repository Authors: NathanP06, TylerLuke024, Kelmyrl