using System;
using System.Collections.Generic;
using System.Text;

namespace Examination_System
{
    public class MultipleChoiceQuestion :Question
    {
        public string CorrectAnswer { get; set; }
        public List<string> Choices = new List<string>();
        public MultipleChoiceQuestion(string header, int marks, QuestionLevel level,List<string> choices, string correctAnswer) : base(header, marks, level)
        {
            this.Choices = choices;
            this.CorrectAnswer = correctAnswer;
        }

        public override void Display()
        {
            Console.WriteLine($"{Header}\t(Multiple Choice)\tMarks:{Marks}");
            for (int i = 0; i < Choices.Count; i++)
            {
                Console.Write($"{i + 1}- {Choices[i]}       ");
            }
        }

        public override bool CheckAnswer(string answer)
        {
            // Convert string to List<int> and remove space at begining and end of each element
            List<int> correctAnswer = CorrectAnswer.Trim().Split(',').Select(s => int.Parse(s.Trim())).ToList();
            List<int> answerStudent = answer.Trim().Split(',').Select(s=>int.Parse(s.Trim())).ToList();

            correctAnswer.Sort();
            answerStudent.Sort();

            int CountCorrectAnswer = 0;
            if(correctAnswer.Count == answerStudent.Count)
            {
                for(int i = 0; i < correctAnswer.Count; i++)
                {
                    if (correctAnswer[i] == answerStudent[i])
                    {
                        CountCorrectAnswer++;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            if(CountCorrectAnswer == correctAnswer.Count)
            {
                return true;
            }
            return false;
        }
    }
}
