$(function () {
    //markdown options
    var mdOptions = {
        //GFM
        gfm: true,
        tables: true,
        breaks: true,
        
        //MarkTex
        marktex: true,
        
        //original markdown
        pedantic: false,
        sanitize: false,     //raw html
        
        //smarter list behavior
        smartLists: true,
        
        //typographic punctuation
        smartypants: false,
            
        //code highlight
        highlight: function(code, lang){
            return hljs.highlight(lang, code).value;
        },
        
        //math renderer
        math: function(math,inline,lang){
            if (inline) {
                return '<span class="mathjax">\\('+math+'\\)</span>';
            }
            else
                return '<div class="mathjax">\\['+math+'\\]</div>';
        }
    };
    
    //do rendering
    marktex($('#src').text(), mdOptions, function(err,content){
        if (err) {
            alert('parsing error: \n'+err);
        }
        $('#dst').html(content);
        $('.mathjax').each(function(i,v){
            MathJax.Hub.Queue(["Typeset",MathJax.Hub,v]);            
        })
    });
});