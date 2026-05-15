using System;
using System.Collections.Generic;
using TextEditorCommand.Models;
using TextEditorCommand.Models.Commands;

namespace TextEditorCommand
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Text Editor - Command Pattern";

            PrintHeader("COMMAND PATTERN - TEXT EDITOR");
            PrintDescription();

            var editor = new TextEditor();
            var history = new CommandHistory();
            var invoker = new EditorInvoker(history);

            DemonstrateBasicCommands(editor, invoker);
            DemonstrateUndoRedo(editor, invoker, history);
            DemonstrateCommandQueue(editor, invoker);
            DemonstrateMacroCommand(editor, invoker);
            DemonstrateHistory(history);

            Console.WriteLine("\n\nPress any key to exit...");
            Console.ReadKey();
        }

        static void DemonstrateBasicCommands(TextEditor editor, EditorInvoker invoker)
        {
            PrintPhase("1. BASIC COMMANDS");

            editor.DisplayContent();

            invoker.ExecuteCommand(new TypeCommand(editor, "Hello, world!"));
            editor.DisplayContent();

            invoker.ExecuteCommand(new TypeCommand(editor, " I am learning Command pattern."));
            editor.DisplayContent();

            invoker.ExecuteCommand(new CopyCommand(editor, 0, 5));

            invoker.ExecuteCommand(new PasteCommand(editor, editor.GetLength()));
            editor.DisplayContent();

            invoker.ExecuteCommand(new DeleteCommand(editor, 5, 10));
            editor.DisplayContent();
        }

        static void DemonstrateUndoRedo(TextEditor editor, EditorInvoker invoker, CommandHistory history)
        {
            PrintPhase("2. UNDO / REDO");

            Console.WriteLine("\n  Creating sequence of commands:");

            var type1 = new TypeCommand(editor, " First sentence.");
            var type2 = new TypeCommand(editor, " Second sentence.");
            var type3 = new TypeCommand(editor, " Third sentence.");

            invoker.ExecuteCommand(type1);
            invoker.ExecuteCommand(type2);
            invoker.ExecuteCommand(type3);
            editor.DisplayContent();

            var undoCommand = new UndoCommand(history);
            invoker.ExecuteCommand(undoCommand);
            editor.DisplayContent();

            invoker.ExecuteCommand(undoCommand);
            editor.DisplayContent();

            var redoCommand = new RedoCommand(history);
            invoker.ExecuteCommand(redoCommand);
            editor.DisplayContent();

            invoker.ExecuteCommand(redoCommand);
            editor.DisplayContent();
        }

        static void DemonstrateCommandQueue(TextEditor editor, EditorInvoker invoker)
        {
            PrintPhase("3. COMMAND QUEUE");

            invoker.QueueCommand(new TypeCommand(editor, "A"));
            invoker.QueueCommand(new TypeCommand(editor, "B"));
            invoker.QueueCommand(new TypeCommand(editor, "C"));
            invoker.QueueCommand(new DeleteCommand(editor, 1));
            invoker.QueueCommand(new TypeCommand(editor, "X"));

            invoker.ExecuteQueuedCommands();
            editor.DisplayContent();
        }

        static void DemonstrateMacroCommand(TextEditor editor, EditorInvoker invoker)
        {
            PrintPhase("4. MACRO COMMAND");

            var greetingMacro = new List<ICommand>
            {
                new TypeCommand(editor, "\n--- START ---"),
                new TypeCommand(editor, "\nHello!"),
                new TypeCommand(editor, "\nHow are you?"),
                new TypeCommand(editor, "\n--- END ---")
            };

            invoker.ExecuteMacroCommand(greetingMacro, "GREETING");
            editor.DisplayContent();

            var formatMacro = new List<ICommand>
            {
                new CopyCommand(editor, 0, 10),
                new TypeCommand(editor, "\n[COPIED: "),
                new PasteCommand(editor),
                new TypeCommand(editor, "]")
            };

            invoker.ExecuteMacroCommand(formatMacro, "FORMATTING");
            editor.DisplayContent();
        }

        static void DemonstrateHistory(CommandHistory history)
        {
            PrintPhase("5. COMMAND HISTORY");
            history.ShowHistory();
        }

        static void PrintHeader(string title)
        {
            Console.WriteLine($"\n+========================================================+");
            Console.WriteLine($"|  {title,-54} |");
            Console.WriteLine($"+========================================================+");
        }

        static void PrintPhase(string phase)
        {
            Console.WriteLine($"\n+--------------------------------------------------------+");
            Console.WriteLine($"|  {phase,-54} |");
            Console.WriteLine($"+--------------------------------------------------------+");
        }

        static void PrintDescription()
        {
            Console.WriteLine("\n[DESCRIPTION] Command Pattern Components:");
            Console.WriteLine("  - Command Interface: ICommand (Execute, Undo)");
            Console.WriteLine("  - Concrete Commands: Copy, Paste, Type, Delete, Undo, Redo");
            Console.WriteLine("  - Receiver: TextEditor (performs actions)");
            Console.WriteLine("  - Invoker: EditorInvoker (calls commands)");
            Console.WriteLine("  - Storage: CommandHistory (Undo/Redo stacks)");
            Console.WriteLine("\n[ADVANTAGES]");
            Console.WriteLine("  - Undo/Redo support");
            Console.WriteLine("  - Command queueing");
            Console.WriteLine("  - Macro commands");
            Console.WriteLine("  - Separation of invoker and receiver");
        }
    }
}