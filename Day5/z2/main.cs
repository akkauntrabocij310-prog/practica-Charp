class Program
{
    static void Main()
    {
        Professor profIvanov = new Professor("Иванов И.И.");
        Student s1 = new Student("Алексей");
        Student s2 = new Student("Мария");
        Student s3 = new Student("Дмитрий");
        University[] universities = new University[]
        {
            new University("МГУ", "Мехмат", new Student[] { s1, s2 }),
            new University("МГТУ", "Робототехника", new Student[] { s3 })
        };
        universities[0].Teacher = profIvanov;
        universities[1].Teacher = profIvanov;
        foreach (var uni in universities)
        {
            uni.ShowStudents();
        }
    }
}