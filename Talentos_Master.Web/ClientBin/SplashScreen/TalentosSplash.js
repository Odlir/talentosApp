function onSourceDownloadProgressChanged(sender, eventArgs) {


    var totalWidth = document.body.clientWidth;
    var totalHeight = document.body.clientHeight;


    var loader = sender.getHost().content.findName("parentCanvas");
    loader.Width = totalWidth;
    loader.Height = totalHeight;

    sender.findName("internalCanvas").setValue("Canvas.Left", Math.round((totalWidth - 1002) / 2));


    //sender.findName("uxStatus").Text =  Math.round((eventArgs.progress * 1000)) / 100 + " %";
    sender.findName("uxStatus").Text = Math.round((eventArgs.progress * 1000) / 10) + " %";

    if (Math.round(eventArgs.progress * 100) == 1) {
        sender.findName("Storyboard2").Begin();
        sender.findName("IndicatorStoryboard").Begin();
        //sender.findName("Storyboard1").Begin();

}

    if (Math.round(eventArgs.progress * 100) == 100) {
        sender.findName("uxStatus").Text = "Cargando...";
    }
}
