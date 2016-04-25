using System;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui;
using Mono.TextEditor;
using XSWebSnippet.Shared;

namespace XSWebSnippet
{
	public class CopyHandler : CommandHandler
	{
		protected override void Run()
		{
			Document doc = IdeApp.Workbench.ActiveDocument;
			var textEditorData = doc.GetContent<ITextEditorDataProvider>().GetTextEditorData();
			string[] text = textEditorData.SelectedText.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

			var documentType = this.GetDocumentType(textEditorData.MimeType);
			TextProcessor processor = new TextProcessor();
			string copyText = processor.Process(text, documentType);

			Gtk.Clipboard.
		}

		protected override void Update(CommandInfo info)
		{
			bool enabled = false;
			Document doc = IdeApp.Workbench.ActiveDocument;
			var textEditorData = doc.GetContent<ITextEditorDataProvider>().GetTextEditorData();

			if (textEditorData != null && textEditorData.IsSomethingSelected)
			{
				enabled = true;
			}

			info.Enabled = enabled;
		}

		private DocumentType GetDocumentType(string mimeType)
		{
			switch (mimeType.ToLower())
			{
				case "text/x-csharp":
					return DocumentType.CSharp;

				case "application/xml":
					return DocumentType.Xml;

				default:
					return DocumentType.Unknown;
			}
		}
	}
}

