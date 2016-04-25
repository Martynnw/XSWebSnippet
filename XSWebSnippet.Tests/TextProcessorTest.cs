using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using XSWebSnippet.Shared;

namespace XSWebSnippet.Tests
{
	[TestFixture()]
	public class TextProcessorTest
	{
		TextProcessor processor;

		[SetUp]
		public void Init()
		{
			processor = new TextProcessor();
		}

		[Test()]
		public void ReplaceTabs_SingleLine_Replaced()
		{
			var text = new List<string> { "\t\tBlah" };

			processor.ReplaceTabs(text);

			Assert.AreEqual("        Blah", text[0]);
		}

		[Test()]
		public void SetAlignment_SingleLine_Realign()
		{
			var text = new List<string> { "   Blah" };

			processor.SetAlignment(text);

			Assert.AreEqual("Blah", text[0]);
			Assert.AreEqual(1, text.Count());
		}

		[Test()]
		public void SetAlignment_MultiLine_Realign()
		{
			var text = new List<string> {   "     Blah",
											"  Blah",
											"    Blah"};

			processor.SetAlignment(text);

			Assert.AreEqual("   Blah", text[0]);
			Assert.AreEqual("Blah", text[1]);
			Assert.AreEqual("  Blah", text[2]);
			Assert.AreEqual(3, text.Count());
		}

		[Test()]
		public void SetAlignement_MultiLine_NoChange()
		{
			var text = new List<string> {   "  Blah",
											" Blah",
											"Blah"};

			processor.SetAlignment(text);

			Assert.AreEqual("  Blah", text[0]);
			Assert.AreEqual(" Blah", text[1]);
			Assert.AreEqual("Blah", text[2]);
			Assert.AreEqual(3, text.Count());
		}

		[Test()]
		public void SetAlignement_MultiLineEmptyLine_Realign()
		{
			var text = new List<string> {   "  Blah",
											"",
											" Blah"};

			processor.SetAlignment(text);

			Assert.AreEqual(" Blah", text[0]);
			Assert.AreEqual("", text[1]);
			Assert.AreEqual("Blah", text[2]);
			Assert.AreEqual(3, text.Count());
		}

		[Test()]
		public void HtmlEncode_MultiLine_Encode()
		{
			var text = new List<string> {   "List<string> foo = new List<string>();",
											"    string blah = \"Fish & Chips\";",
											"string foo = string.empty;"};

			processor.HtmlEncode(text);

			Assert.AreEqual("List&lt;string&gt; foo = new List&lt;string&gt;();", text[0]);
			Assert.AreEqual("    string blah = &quot;Fish &amp; Chips&quot;;", text[1]);
			Assert.AreEqual("string foo = string.empty;", text[2]);
			Assert.AreEqual(3, text.Count());
		}
	}
}

