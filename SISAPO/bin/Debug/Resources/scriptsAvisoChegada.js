<!--
// *****Define o estilo*****
function definecss(caminho){

var mac = (navigator.platform.indexOf("Mac") != -1);
var pc = (navigator.platform.indexOf("Win") != -1);

var ns = (document.layers) ? true : false;
var ie = (document.all) ? true : false;

if (mac && ns) {
 estilo = '<link rel="stylesheet" href="scriptcss/cssMacNS.css" type="text/css">';
}
else if (mac && ie) {
 estilo = '<link rel="stylesheet" href="scriptcss/cssMacIE.css" type="text/css">';
}
else if (pc && ns) {
 estilo = '<link rel="stylesheet" href="scriptcss/cssPcNS.css" type="text/css">';
}
else if (pc && ie) {
 estilo = '<link rel="stylesheet" href="' + caminho + 'cssPcIE.css" type="text/css">';
}
else if ((!mac && !pc) && ns) {
 estilo = '<link rel="stylesheet" href="scriptcss/cssPcIE.css" type="text/css">';
} else {
 estilo = '<link rel="stylesheet" href="scriptcss/cssPcIE.css" type="text/css">';
}
document.write(estilo);
}
// *****Fim script estilos*****
// *****Funções default do Dreamweaver*****

function MM_swapImgRestore() { //v3.0
  var i,x,a=document.MM_sr; for(i=0;a&&i<a.length&&(x=a[i])&&x.oSrc;i++) x.src=x.oSrc;
}

function MM_preloadImages() { //v3.0
  var d=document; if(d.images){ if(!d.MM_p) d.MM_p=new Array();
    var i,j=d.MM_p.length,a=MM_preloadImages.arguments; for(i=0; i<a.length; i++)
    if (a[i].indexOf("#")!=0){ d.MM_p[j]=new Image; d.MM_p[j++].src=a[i];}}
}

function MM_findObj(n, d) { //v4.0
  var p,i,x;  if(!d) d=document; if((p=n.indexOf("?"))>0&&parent.frames.length) {
    d=parent.frames[n.substring(p+1)].document; n=n.substring(0,p);}
  if(!(x=d[n])&&d.all) x=d.all[n]; for (i=0;!x&&i<d.forms.length;i++) x=d.forms[i][n];
  for(i=0;!x&&d.layers&&i<d.layers.length;i++) x=MM_findObj(n,d.layers[i].document);
  if(!x && document.getElementById) x=document.getElementById(n); return x;
}

function MM_swapImage() { //v3.0
  var i,j=0,x,a=MM_swapImage.arguments; document.MM_sr=new Array; for(i=0;i<(a.length-2);i+=3)
   if ((x=MM_findObj(a[i]))!=null){document.MM_sr[j++]=x; if(!x.oSrc) x.oSrc=x.src; x.src=a[i+2];}
}

function MM_reloadPage(init) {  //reloads the window if Nav4 resized
  if (init==true) with (navigator) {if ((appName=="Netscape")&&(parseInt(appVersion)==4)) {
    document.MM_pgW=innerWidth; document.MM_pgH=innerHeight; onresize=MM_reloadPage; }}
  else if (innerWidth!=document.MM_pgW || innerHeight!=document.MM_pgH) location.reload();
}
MM_reloadPage(true);

function MM_openBrWindow(theURL,winName,features) { //v2.0
  window.open(theURL,winName,features);
}
// *****Fim de funções default do Dreamweaver*****
//-->
