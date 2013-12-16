//config file for marktex
//config and parse markdown content

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
function update(mdContent){
    mdContent = decodeURI(mdContent);
    marktex(mdContent, mdOptions, function(err,content){
        if (err) {
            alert('parsing error: \n'+err);
        }
        $('#content').html(content);
        $('#content .mathjax').each(function(i,v){
            MathJax.Hub.Queue(["Typeset",MathJax.Hub,v]);            
        })
    });    
}
