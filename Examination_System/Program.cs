
using System.Xml;
using static System.Net.WebRequestMethods;

namespace Examination_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int numberChoose;
            List<Question> questions = new List<Question>();
            do
            {
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("Main Menu:");
                Console.WriteLine("1- Doctor Mode");
                Console.WriteLine("2- Student Mode");
                Console.WriteLine("3- Exit");
                try
                {
                    Console.Write("Choose: ");
                    numberChoose = Convert.ToInt32(Console.ReadLine());

                    if (numberChoose > 3 || numberChoose < 1)
                    {
                        throw new Exception("Invalid option !");
                    }
                    if (numberChoose == 1)
                    {
                        DoctorMode(questions);
                    }
                    else if (numberChoose == 2)
                    {
                        StudentMode(questions);
                    }
                    else if (numberChoose == 3)
                    {
                        Console.WriteLine("Program End\n");
                    }
                }
                catch(Exception ex)
                {
                    numberChoose = -1;
                    Console.WriteLine(ex.Message + "\n");
                }
            } while (numberChoose != 3);
        }

        public static void DoctorMode(List<Question> questions)
        {
            try
            {
                Console.Write("How many question do you want to add? ");
                int numberQuestion = Convert.ToInt32(Console.ReadLine());
                if(numberQuestion <= 0)
                {
                    throw new Exception("Invalid number !");
                }

                for (int i = 0; i < numberQuestion; i++)
                {
                    Console.WriteLine($"Adding Question {i + 1}:");
                    Console.WriteLine("Select Question Type: 1- True/False, 2- Choose One, 3- Multiple Choice");
                    int numberQuestionType = Convert.ToInt32(Console.ReadLine());
                    if(numberQuestionType < 1 || numberQuestionType > 3)
                    {
                        throw new Exception("Invalid option !");
                    }

                    Console.Write("Enter Question Level (easy/medium/hard): ");
                    QuestionLevel level = Enum.Parse<QuestionLevel>(Console.ReadLine()!, true); // convert string to enum(QuestionLevel) 

                    Console.Write("Enter Question Header: ");
                    string header = Console.ReadLine()!;
                    if(string.IsNullOrEmpty(header)) throw new Exception("Header is empty !");

                    Console.Write("Enter Question Marks: ");
                    int marks = Convert.ToInt32(Console.ReadLine());
                    if(marks <= 0)
                    {
                        throw new Exception("Invalid marks !");
                    }

                    List<string> choices = new List<string>();

                    if (numberQuestionType == 1)
                    {
                        Console.Write("Enter Correct Answer (true/false): ");
                        string? correctAnswer = Console.ReadLine();
                        if(string.IsNullOrEmpty(correctAnswer)) throw new Exception("Correct answer is empty !");
                        questions.Add(new TrueFalseQuestion(header, marks, level, correctAnswer));
                    }

                    else if (numberQuestionType == 2)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            Console.Write($"Enter choice {j + 1}:");
                            string? choice = Console.ReadLine();
                            if (string.IsNullOrEmpty(choice)) throw new Exception("Choice is empty !");
                            choices.Add(choice);
                        }

                        Console.Write("Enter correct choice number (1-4):");
                        string? correctAnswer = Console.ReadLine();
                        if(string.IsNullOrEmpty(correctAnswer)) throw new Exception("Correct answer is empty !");
                        if (correctAnswer != "1" && correctAnswer != "2" && correctAnswer != "3" && correctAnswer != "4")
                        {
                            throw new Exception("Invalid choice number !");
                        }

                        questions.Add(new ChooseOneQuestion(header, marks, level, choices, correctAnswer));
                    }

                    else if (numberQuestionType == 3)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            Console.Write($"Enter choice {j + 1}:");
                            string? choice = Console.ReadLine();
                            if (string.IsNullOrEmpty(choice)) throw new Exception("Choice is empty !");
                            choices.Add(choice);
                        }

                        Console.Write("Enter correct answers (comma separated, e.g. 1,3): ");
                        string? correctAnswer = Console.ReadLine();
                        if(string.IsNullOrEmpty(correctAnswer)) throw new Exception("Correct answer is empty !");
                        List<int> listCorrectAnsswer = correctAnswer.Split(',').Select(s => int.Parse(s.Trim())).ToList();
                        foreach (int answer in listCorrectAnsswer)
                        {
                            if (answer < 1 || answer > 4)
                            {
                                throw new Exception("Invalid choice number !");
                            }
                        }
                        questions.Add(new MultipleChoiceQuestion(header, marks, level, choices, correctAnswer));
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + "\n");
            }

        }

        public static void StudentMode(List<Question> questions)
        {
            Console.WriteLine("Choose Exam Type: 1-Practical Exam, 2-Final Exam");
            int numberChooseExam = Convert.ToInt32(Console.ReadLine());
            if(numberChooseExam < 1 || numberChooseExam > 2)
            {
                throw new Exception("Invalid option !");
            }

            Console.Write("Enter Exam Level (easy/medium/hard): ");
            QuestionLevel level = Enum.Parse<QuestionLevel>(Console.ReadLine()!, true);

            Console.WriteLine("\n--- Exam Started ---\n");

            if (numberChooseExam == 1)
            {
                int TotalMarks = 0;
                int StudentMarks = 0;
                List<Question> result = questions.FindAll(q => q.Level == level);
                for(int i = 0; i < result.Count / 2; i++)
                {
                    result[i].Display();

                    Console.Write("\n\nYour Answer:");
                    string? answer = Console.ReadLine();
                    if(string.IsNullOrEmpty(answer)) throw new Exception("Answer is empty !");

                    TotalMarks += result[i].Marks;

                    if (result[i].CheckAnswer(answer))
                    {
                        StudentMarks += result[i].Marks;
                    }
                }

                Console.WriteLine($"Your result : {StudentMarks} / {TotalMarks}");
            }

            else if(numberChooseExam == 2)
            {
                int TotalMarks = 0;
                int StudentMarks = 0;
                List<Question> result = questions.FindAll(q => q.Level == level);
                for (int i = 0; i < result.Count; i++)
                {
                    result[i].Display();
                    Console.Write("\n\nYour Answer:");
                    string? answer = Console.ReadLine();
                    if(string.IsNullOrEmpty(answer)) throw new Exception("Answer is empty !");

                    TotalMarks += result[i].Marks;

                    if (result[i].CheckAnswer(answer))
                    {
                        StudentMarks += result[i].Marks;
                    }
                }

                Console.WriteLine($"Your result : {StudentMarks} / {TotalMarks}");
            }
        }
    }
}
