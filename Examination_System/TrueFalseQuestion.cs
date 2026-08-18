namespace Examination_System
{
    public class TrueFalseQuestion : Question
    {
        public string CorrectAnswer {  get; set; }
        public TrueFalseQuestion(string header, int marks , QuestionLevel level, string correctAnswer) : base(header, marks, level)
        {
            this.CorrectAnswer = correctAnswer;
        }

        public override void Display()
        {
            Console.WriteLine($"{Header}\t(True/False)\tMarks:{Marks}");
        }

        public override bool CheckAnswer(string answer)
        {
            // to Comparision two string without considering Uppercase or Lowercase letters
            if (answer.Trim().Equals(CorrectAnswer, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }

    }
}
