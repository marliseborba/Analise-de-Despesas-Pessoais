using Expenses.Services;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text.Json.Nodes;

namespace Expenses.Models.Charts
{

	public class Chart
	{
		public string type { get; set; } = "line";
		public Data data { get; set; }
		public Option options { get; set; } =  new Option();

    public Chart()
		{
        }

		public Chart(string type, Data data)
		{
			this.type = type;
            //change type to area, bar, bubble, doughnut, line, mixed, pie, polar, radar, scatter
            this.data = data;
		}
	}

    public class Option
    {
		//public Scale scales { get; set; } = new Scale();
		public Plugins plugins { get; set; } = new Plugins();
		public Animation animation { get; set; } = new Animation();
		public int borderRadius { get; set; }
    }

    public class Scale
    {
		public YAxis yAxis { get; set; } = new YAxis();
    }

	public class YAxis
	{
		public Tick ticks { get; set; } = new Tick();
	}

	public class Tick
	{
		public string callback { get; set; } = "";
		public string title { get; set; } = "";
	}

	public class Plugins
	{
		//public Tooltip tooltip { get; set; } = new Tooltip();
		public Title title { get; set; } = new Title();
		public Subtitle subtitle { get; set; } = new Subtitle();
		public Legend legend { get; set; } = new Legend();
	}

	public class Animation
	{
		public string easing { get; set; }
	}

	public class Tooltip
	{
		public Calbacks calbacks { get; set; } = new Calbacks();
	}

	public class Title
	{
		public bool display { get; set; } = true;
		public string text { get; set; } = "";
		public Font font { get; set; } = new Font(20);

		public Title()
		{
		}

        public Title(string text)
        {
            this.text = text;
        }
    }

    public class Subtitle
    {
        public bool display { get; set; } = true;
        public List<string> text { get; set; } = new List<string>();
        public Font font { get; set; } = new Font(16);

		public Subtitle()
		{
		}

        public Subtitle(string text)
        {
            this.text.Add(text);
        }
    }

    public class Legend
	{
		public Label labels { get; set; } = new Label();
	}

	public class Label
	{
		public string pointStyle { get; set; } = "circle";
		public bool usePointStyle { get; set; } = true;
	}

	public class Font
	{
		public int size { get; set; } = 20;

        public Font(int size)
        {
            this.size = size;
        }
    }

	public class Calbacks
	{
		public string label { get; set; } = "";
	}
}
