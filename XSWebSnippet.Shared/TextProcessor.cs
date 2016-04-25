using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XSWebSnippet.Shared
{
	public class TextProcessor
	{
		private const int TabSpaces = 4;

		public TextProcessor()
		{
		}

		public string Process(string[] source, DocumentType codeType)
		{
			List<string> lines = source.ToList();
			this.ReplaceTabs(lines);
			this.SetAlignment(lines);
			this.HtmlEncode(lines);
			this.ProcessForHighlighter(lines, codeType);

			var bob = new StringBuilder();
			lines.ForEach(x => bob.Append(x));

			return bob.ToString();
		}

		public void ReplaceTabs(List<string> lines)
		{
			string spaces = new string(' ', TabSpaces);

			for (int i = 0; i < lines.Count(); i++)
			{
				lines[i] = lines[i].Replace("\t", spaces);
			}
		}

		public void SetAlignment(List<string> lines)
		{
			int minSpaces = -1;

			foreach (string line in lines)
			{
				int spaces = -1;

				for (int i = 0; i < line.Count(); i++)
				{
					if (line[i] != ' ')
					{
						spaces = i;
						break;
					}
				}

				if (spaces != -1)
				{
					if (minSpaces == -1 || minSpaces > spaces)
					{
						minSpaces = spaces;
					}
				}
			}

			if (minSpaces > 0)
			{
				for (int i = 0; i < lines.Count(); i++)
				{
					if (lines[i] != string.Empty)
					{
						lines[i] = lines[i].Substring(minSpaces);
					}
				}
			}
		}

		public void HtmlEncode(List<string> lines)
		{
			for (int i = 0; i < lines.Count(); i++)
			{
				lines [i] = System.Web.HttpUtility.HtmlEncode (lines[i]);
			}
		}

		public virtual void ProcessForHighlighter(List<string> lines, DocumentType codeType)
		{
		}
	}
}

