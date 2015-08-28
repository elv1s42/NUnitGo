using System;

namespace HtmlCustomElements.HtmlCustomElements
{
    public class OpenModalWindowScript
    {
        public string Script;

        public OpenModalWindowScript()
        {
            Script = GetScript();
        }

        private static string GetScript()
        {
            return "function openModalWindow(file, idToOpen, idToFill, bcgId) {" + Environment.NewLine
                + "document.getElementById(idToOpen).style.display='block';" + Environment.NewLine
                + "document.getElementById(bcgId).style.display='block';" + Environment.NewLine
                + "document.getElementsByTagName('body')[0].className+=' stop-scrolling';" + Environment.NewLine
                + @"var fileReader = new FileReader();
	            fileReader.onload = function(fileLoadedEvent) 
	            {
		            var textFromFileLoaded = fileLoadedEvent.target.result;
		            document.getElementById(idToFill).innerText = textFromFileLoaded;
	            };
	            fileReader.readAsText(file, 'UTF-8');" + Environment.NewLine
                + "}" + Environment.NewLine;
        }
    }
}
