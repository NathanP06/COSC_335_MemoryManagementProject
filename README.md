# COSC_335_MemoryManagementProject
A simple memory management project for my COSC 335 - Operating Systems class

## About `Program.cs`

`Program.cs` is a small, educational console program that demonstrates three core memory concepts in C#:

- Stack memory (via recursion)
- Heap memory (via object allocation and references)
- Buffering (reading data in fixed-size chunks)

The program prints clear, annotated output for each demo so it can be used in class presentations or for self-study.

### Files

- `COSC_335_MemoryManagementProject/Program.cs` — the main demo program with three examples: `StackExample`, `HeapExample`, and `BufferExample`.

## How to run

From the repository root (PowerShell on Windows):

```powershell
# Build the solution
dotnet build

# Run the console demo (project path may be adjusted if needed)
dotnet run --project .\COSC_335_MemoryManagementProject\COSC_335_MemoryManagementProject.csproj
```

The program will run the three demos in order and print explanatory text to the console.

## What each demo shows

- StackExample(int depth)
	- Demonstrates stack frames through recursion. Each recursive call creates a new frame and local variables live on the stack. When recursion returns, frames are popped (LIFO behavior).

- HeapExample()
	- Shows object allocation on the heap using the `Dog` class. Two Dog objects are created and then their references are set to `null`, illustrating how objects become eligible for garbage collection.

- BufferExample()
	- Simulates stream processing with a fixed-size buffer (10 bytes). Shows how data is read in chunks from a MemoryStream and how buffers help manage memory when processing streams.

## How to present this to a class (quick tips)

- Run the program and pause after each demo to explain what's printed.
- For the stack demo, draw stack frames on the board as the recursion goes deeper and then unwinds.
- For the heap demo, draw heap objects and reference arrows. Emphasize that setting a reference to `null` only removes the reference; the GC decides when to reclaim the memory.
- For the buffer demo, draw a fixed-size buffer and show how each chunk fills and empties the buffer.

## Observing the .NET Garbage Collector (non-invasive)

If you want to *observe* the garbage collector in action without changing `Program.cs`, use one of these tools from another PowerShell window while the demo runs:

- dotnet-counters (live counters)
	- List .NET processes:
		```powershell
		dotnet-counters ps
		```
	- Monitor GC-related counters (replace `<PID>`):
		```powershell
		dotnet-counters monitor -p <PID> System.Runtime
		```
	- Watch counters such as `gen-0-gc-count`, `allocated-bytes`, and `time-in-gc`.

- dotnet-trace (record and analyze)
	- Record a trace while the app runs (replace `<PID>`):
		```powershell
		dotnet-trace collect -p <PID> --providers Microsoft-DotNETRuntime:4:5 -o trace.nettrace
		```
	- Open `trace.nettrace` in PerfView or Visual Studio for GC events and timings.

- dotnet-gcdump (heap snapshot)
	- Collect a GC heap dump:
		```powershell
		dotnet-gcdump collect -p <PID> -o myheap.gcdump
		```
	- Open the `.gcdump` in Visual Studio to inspect heap size and object types.

- Visual Studio Diagnostics
	- Run under the debugger and open the Diagnostic Tools. Use the Memory tab to take snapshots and force GC. This provides a visual view of objects, retention graphs, and allocation stacks.

- PerfView
	- Use PerfView for detailed GC/heap analysis and allocation stacks. Collect GC traces and view heaps and GC pauses.

Notes:
- If your demo run creates only a few objects the GC may not run frequently. You can repeatedly run `Program.cs` or attach a runtime tool while it runs to observe collection activity.

Important: This repository's intent is to keep the demonstrations focused in `Program.cs`. The guidance above uses only non-invasive runtime tools (dotnet-counters, dotnet-trace, dotnet-gcdump, Visual Studio Diagnostics, PerfView).
