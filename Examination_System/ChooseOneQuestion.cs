using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Examination_System
{
    public class ChooseOneQuestion : Question
    {
        public string CorrectAnswer { get; set; }
        public List<string> Choices = new List<string>();
        public ChooseOneQuestion(string header, int marks, QuestionLevel level, List<string> choices,  string correctAnswer) : base(header, marks, level)
        {
            this.Choices = choices;
            this.CorrectAnswer = correctAnswer;
        }

        public override void Display() 
        {
            Console.WriteLine($"{Header}       (Choose One)       Marks:{Marks}");
            for (int i = 0; i < Choices.Count; i++)
            {
                Console.Write($"{i + 1}- {Choices[i]}       ");
            }
        }

        public override bool CheckAnswer(string answer)
        {
            // to Comparision two string without considrting Uppercase or Lowercase letters
            if (answer.Trim().Equals(CorrectAnswer, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }
    }
}
