# COSC_335_MemoryManagementProject

Memory Management Demo (C#)

This project demonstrates basic memory management concepts in C# using small, focused examples:

- Stack behavior and a simulated stack-overflow recursion example.
- Heap usage via objects and garbage collection eligibility.
- Buffering and streaming examples (MemoryStream, FileStream, BufferedStream, StreamReader).

Files of interest
-----------------

- `Project Files (.cs)/Program.cs` — Entry point. Runs the demos in sequence:
  - Stack demo
  - Heap demo
  - Buffer demo (in-memory string)
  - Buffer demo 2 (reads `projecttext.txt`)

- `Project Files (.cs)/StackExample.cs` — Simulates a stack (LIFO) using `Stack<T>` and shows pushing/popping values. Also contains a recursive method `CauseStackOverflow` used to illustrate how an unbounded recursion would overflow the call stack.

- `Project Files (.cs)/HeapExample.cs` — Demonstrates heap allocation with `Table` and `Order` objects. Shows assigning and clearing references and explains when objects become eligible for garbage collection.

- `Project Files (.cs)/BufferExample.cs` — Reads a long string using a `MemoryStream` and a fixed-size byte buffer (10 bytes). Prints each chunk to illustrate chunked reads and how buffers reduce memory pressure and I/O cost.

- `Project Files (.cs)/BufferExample2.cs` — Reads the file `projecttext.txt` using `FileStream`, `BufferedStream`, and `StreamReader`. Prints the file lines to the console. Note: the code currently uses an absolute path to `projecttext.txt` — see the "File path" note below.

- `projecttext.txt` — A text file included in the project containing an explanatory write-up about stack vs heap, buffering, and best practices.

What the program does (high level)
---------------------------------

When you run the program it writes descriptive output to the console describing each demo and then executes the demo. The demos are intentionally simple and verbose so a reader can follow how memory concepts map to C# constructs.

Detailed behaviors
------------------

- Stack demo (`StackExample.Run()`):
  - Creates a `Stack<string>` and pushes five plates (`Plate #1`..`Plate #5`).
  - Iterates the stack to show the LIFO order and then pops items until empty.
  - Calls `CauseStackOverflow(int level)` recursively to illustrate how recursion grows the call stack. The sample code attempts to cap at a high level, but in real .NET a `StackOverflowException` is fatal and usually terminates the process; the example's catch block is not reliable because the runtime will typically terminate before managed code can catch it.

- Heap demo (`HeapExample.Run()`):
  - Creates `Table` objects (heap-allocated) and assigns `Order` objects to them.
  - Displays order details, then clears references with `CloseOrder()` and by setting table variables to `null`.
  - Demonstrates the point at which objects become unreferenced and thus eligible for garbage collection; the demo does not force a GC — it just shows the pattern.

- Buffer demo in memory (`BufferExample.Run()`):
  - Converts a long string to UTF-8 bytes and reads the bytes through a `MemoryStream` using a small fixed-size byte[] buffer (10 bytes).
  - Prints each buffer chunk as text to show how data can be processed in chunks rather than in one big allocation.

- Buffer demo reading file (`BufferExample2.Run()`):
  - Opens `projecttext.txt` using `FileStream` wrapped in `BufferedStream`, then reads text via `StreamReader` line-by-line.
  - Prints each line with its line number. BufferedStream reduces the number of raw file I/O operations by holding chunks in memory.

Example console output (trimmed)
-------------------------------

The real output contains more lines, but here's a representative snippet you can expect when running:

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

How to build and run
--------------------

Open a terminal in the repository root (Windows PowerShell) and run:

```powershell
# Restore (if needed) and build
dotnet build

# Run the app
dotnet run --project "COSC_335_MemoryManagementProject/COSC_335_MemoryManagementProject.csproj"
```

Notes and gotchas
-----------------

- File path: `BufferExample2` uses an absolute path to `projecttext.txt`:

  string filePath = @"C:\Users\natha\OneDrive\Documents\Coding Projects\C# Projects\COSC_335_MemoryManagementProject\COSC_335_MemoryManagementProject\projecttext.txt";

  If you move the repository or share it with others, update `filePath` to either a relative path or compute the path at runtime. Example (relative):

  ```csharp
  string filePath = Path.Combine(AppContext.BaseDirectory, "projecttext.txt");
  ```

- StackOverflow caveat: in .NET the `StackOverflowException` is typically fatal and cannot reliably be caught by managed code; the sample recursion is only educational.

- Disposal: the projects use `using` blocks to ensure streams are disposed promptly — a good practice for unmanaged resources.

Suggested next steps (small improvements)
---------------------------------------

1. Change `BufferExample2` to use a relative path so others can run the demo without editing code.
2. Add a command-line switch to control which demos to run (e.g., `--stack`, `--heap`, `--buffers`).
3. Add a unit test project with a few small tests verifying the non-UI behaviors (e.g., table order assignment logic).

Author and license
------------------

Author: NathanP06

This README is a descriptive guide for the course project and educational use.

---
End of README