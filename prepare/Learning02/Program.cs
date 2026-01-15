using System;

class Program
{
    public class Job{
        public string _company; 
        public string _jobTitle;
        public int _startYear;  
        public int _endYear; 
        public Job(string _company,string _jobTitle,int _startYear,int _endYear)
        {
            this._company = _company;
            this._jobTitle = _jobTitle;
            this._startYear = _startYear;
            this._endYear = _endYear;

        }

        public void Display(){
            Console.WriteLine($"{_jobTitle} ({_company}) {_startYear}-{_endYear}");
        }
    }
    public class Resume
    {
        public string _name;
        public List<Job> _jobs = new List<Job>();

        public void Display()
        {
            Console.WriteLine($"Name: {_name}");
            Console.WriteLine("Jobs:");    
            
            foreach (Job job in _jobs)
            {
               job.Display(); 
            }
        }
    }

    static void Main(string[] args)
    {
        Job job1 = new Job("Microsoft","Software Engineer",2019,2022);
        Job job2 = new Job("Apple","Manager",2022,2023);
        
        Resume resume = new Resume();
        resume._name = "Clark, C";
        resume._jobs.Add(job1);
        resume._jobs.Add(job2);

        resume.Display();

    }
}