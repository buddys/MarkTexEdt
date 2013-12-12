$(function () {
    //custom renderer    
    var r = new marked.Renderer();
    
    //code highlight
    r.code = function(code, lang) {
        if (lang) {
            return '<pre><code class="hljs">'
                + hljs.highlight(lang, code).value
                + '\n</code></pre>';
        }
        else{
            return '<pre><code>'
                + code
                + '\n</code></pre>\n';            
        }
    }

    //markdown options
    var mdOptions = {
        //GFM
        gfm: true,
        tables: true,
        breaks: true,
        
        //original markdown
        pedantic: false,
        sanitize: false,     //raw html
        
        //smarter list behavior
        smartLists: true,
        
        //typographic punctuation
        smartypants: false,
        
        //renderer
        renderer: r,
            
        //math: mathify
        highlight: function(){
            var a=1;
        }
    };
    
    
    
    //renderer complete callback
    mdComplete = function(err,content){
        if (err) throw err;    
        $('#dst').html(content);
    }
    
    //do rendering
    marked($('#src').text(), mdOptions, mdComplete);
});