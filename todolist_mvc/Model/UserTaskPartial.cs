namespace todolist_mvc.Model
{
    public partial class UserTask
    {
        public string DisplayDate
        {
            get 
            {
                DateOnly DeadlineAsDateOnly;
                if (DateOnly.TryParse(Deadline, out DateOnly result))
                {
                    DeadlineAsDateOnly = result;
                }

                string displayDate = $"{DeadlineAsDateOnly.Year}г. {DeadlineAsDateOnly.Day} {monthNames[DeadlineAsDateOnly.Month - 1]}";
                return displayDate;
            }
        }

        string[] monthNames = new string[] 
        {
            "января", "февраля", "марта",
            "апреля", "мая", "июня", "июля",
            "августа", "сентября", "октября",
            "ноября", "декабря"
        };


        public string Abrakadabra 
        { 
            get 
            { 
                return "qweqweqwe"; 
            }
        }


    }
}
