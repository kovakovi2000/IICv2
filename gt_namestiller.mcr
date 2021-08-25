DELAY : 100
Mouse : 166 : 861 : Click : 0 : 0 : 0
SET CLIPBOARD : var n1 = document.getElementsByClassName("table_lst table_lst_spn")[0].rows.length;{#crlf#}    {#crlf#}    var i=0,j=0;{#crlf#}    var str="";{#crlf#}     {#crlf#}    for(i=1; i<n1;i++){#crlf#}    {{#crlf#}        var n2 = document.getElementsByClassName("table_lst table_lst_spn")[0].rows[i].cells.length;{#crlf#}        {#crlf#}        var name = document.getElementsByClassName("table_lst table_lst_spn")[0].rows[i].cells.item(1).innerHTML;{#crlf#}        var s = name.split("\n");{#crlf#}{#crlf#}        str=str+s[2].substring(6).replace("&lt;", "<").replace("&gt;", ">")+"\n";{#crlf#}        {#crlf#}    }{#crlf#}    console.log(str);{#crlf#}    var tempInput = document.createElement("textarea");{#crlf#}    tempInput.value = str.substr(0, (str.length - 1));{#crlf#}    document.body.appendChild(tempInput);{#crlf#}    tempInput.select();{#crlf#}    document.execCommand("copy");{#crlf#}    document.body.removeChild(tempInput);{#crlf#}    var aTags = document.getElementsByTagName("a");{#crlf#}    var searchText = "NEXT";{#crlf#}    var found;{#crlf#}{#crlf#}    for (var i = 0; i < aTags.length; i++) {{#crlf#}    if (aTags[i].textContent == searchText) {{#crlf#}        found = aTags[i];{#crlf#}        break;{#crlf#}    }{#crlf#}    }{#crlf#}    window.location.href = found.href; : 0 : Please enter the text to store in clipboard:
PASTE
Keyboard : Enter : KeyPress
DELAY : 1000
Mouse : 750 : 17 : Click : 0 : 0 : 0
Keyboard : ControlLeft : KeyDown
Keyboard : V : KeyPress
Keyboard : S : KeyPress
Keyboard : ControlLeft : KeyUp
