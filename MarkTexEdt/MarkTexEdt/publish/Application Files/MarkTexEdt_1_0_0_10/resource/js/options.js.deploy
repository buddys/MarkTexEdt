highlight: function(code, lang){
    return hljs.highlight(lang,code).value;
},
math: function(math,inline,lang){
    if (inline) {
        return '<span class="mathjax">\\('+math+'\\)</span>';
	}
    else
        return '<div class="mathjax">\\['+math+'\\]</div>';
}