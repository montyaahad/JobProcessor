namespace JobProcessor.JobProcessLib
{
    using System;

    public class SingleJobSequence
    {
        private readonly string _name;

        private readonly string _dependsOn;

        public SingleJobSequence(string input)
        {
            var splitted = input.Trim(' ').Split(new string[] { "=>" }, StringSplitOptions.None);
            this._name = splitted[0];
            this._dependsOn = splitted[1];
        }

        public string GetJobName()
        {
            return this._name;
        }

        public string GetDependsOnName()
        {
            return this._dependsOn;
        }
    }
}
