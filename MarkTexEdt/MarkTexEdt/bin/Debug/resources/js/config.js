//$(function () {
//    var $content = $('#content');

//    var markedOptions = {
//        gfm: true,
//        pedantic: false,
//        sanitize: false,
//        tables: true,
//        smartLists: true,
//        breaks: true
//        /*langPrefix: 'language-',
//        math: mathify,
//        highlight: function (codeText, codeLanguage) { return highlightSyntax(hljs, codeText, codeLanguage);*/
//    };
//    //$content.html( marked($content.html(), markedOptions));
//});

window.onload = function () {
    var markedOptions = {
        gfm: true,
        pedantic: false,
        sanitize: false,
        tables: true,
        smartLists: true,
        breaks: true
        /*langPrefix: 'language-',
        math: mathify,
        highlight: function (codeText, codeLanguage) { return highlightSyntax(hljs, codeText, codeLanguage);*/
    };
    var content = document.getElementById("src");
    var content = content.textContent;
    document.getElementById("dst").innerHTML = marked(content, markedOptions);
};