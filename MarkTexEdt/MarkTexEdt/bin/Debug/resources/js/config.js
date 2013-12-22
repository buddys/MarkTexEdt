//do rendering
function update(mdContent,Options){
    marktex( decodeURI(mdContent), Options, function(err,content){
        if (err) return;
        $('#content').html(content);
        $('#content .mathjax').each(function(i,v){
            MathJax.Hub.Queue(["Typeset",MathJax.Hub,v]);            
        })
    });    
}
