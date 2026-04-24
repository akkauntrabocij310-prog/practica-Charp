class Program
{
    static void Main()
    {
        GraphicEditor editor = new GraphicEditor();
        IDraw drawer = editor;
        drawer.ApplyColor("Черный (Black)");
        IPaint painter = editor;
        painter.ApplyColor("Красный (Red)");
        ((IDraw)editor).ApplyColor("Синий (Blue)");
        ((IPaint)editor).ApplyColor("Зеленый (Green)");
        Console.WriteLine("\nПримечание: Прямой вызов editor.ApplyColor() невозможен.");
    }
}