//do rendering
function update(mdContent,Options){
    mdContent = decodeURI(mdContent);
	Options = decodeURI(Options);
    marktex(mdContent, Options, function(err,content){
        if (err) {
			return;
        }
        $('#content').html(content);
        $('#content .mathjax').each(function(i,v){
            MathJax.Hub.Queue(["Typeset",MathJax.Hub,v]);            
        })
    });    
}
