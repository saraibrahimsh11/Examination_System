using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Examination_System
{
    public enum QuestionLevel
    {
        easy,     // by default = 0
        medium,   // by default = 1
        hard,     // by default = 2
    }
    public abstract class Question
    {
        public Question(string header, int marks, QuestionLevel level)
        {
            this.Header = header;
            this.Marks = marks;
            this.Level = level;
        }

        public string Header { get; set; }
        public int Marks { get; set; }
        public QuestionLevel Level { get; set; }

        public abstract void Display();
        public abstract bool CheckAnswer(string answer);
    }
}
