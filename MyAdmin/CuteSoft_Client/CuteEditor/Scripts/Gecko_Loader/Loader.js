var OxOc9ba=["ua","userAgent","isOpera","opera","isSafari","safari","isGecko","gecko","isWinIE","msie","compatMode","document","CSS1Compat","undefined","Microsoft.XMLHTTP","readyState","onreadystatechange","","length","all","childNodes","nodeType","\x0D\x0A","caller","onchange","oninitialized","command","commandui","commandvalue","returnValue","oncommand","string","_fireEventFunction","event","parentNode","_IsCuteEditor","True","readOnly","_IsRichDropDown","null","value","selectedIndex","nodeName","TR","cells","display","style","nextSibling","innerHTML","\x3Cimg src=\x22","/Load.ashx?type=image\x26file=t-minus.gif\x22\x3E","onclick","CuteEditor_CollapseTreeDropDownItem(this,\x22","\x22)","none","/Load.ashx?type=image\x26file=t-plus.gif\x22\x3E","CuteEditor_ExpandTreeDropDownItem(this,\x22","contains","UNSELECTABLE","on","tabIndex","-1","//TODO: event not found? throw error ?","contentWindow","contentDocument","parentWindow","id","frames","frameElement","//TODO:frame contentWindow not found?","preventDefault","arguments","parent","top","opener","head","script","language","javascript","type","text/javascript","src","srcElement","target","//TODO: srcElement not found? throw error ?","fromElement","relatedTarget","toElement","keyCode","clientX","clientY","offsetX","offsetY","button","ctrlKey","altKey","shiftKey","cancelBubble","stopPropagation","CuteEditor_GetEditor(this).ExecImageCommand(this.getAttribute(\x27Command\x27),this.getAttribute(\x27CommandUI\x27),this.getAttribute(\x27CommandArgument\x27),this)","CuteEditor_GetEditor(this).PostBack(this.getAttribute(\x27Command\x27))","this.onmouseout();CuteEditor_GetEditor(this).DropMenu(this.getAttribute(\x27Group\x27),this)","ResourceDir","Theme","/Load.ashx?type=theme\x26theme=","\x26file=all.png","/Images/blank2020.gif","IMG","alt","title","Command","Group","ThemeIndex","width","20px","height","backgroundImage","url(",")","backgroundPosition","0 -","px","onload","className","separator","CuteEditorButton","onmouseover","CuteEditor_ButtonCommandOver(this)","onmouseout","CuteEditor_ButtonCommandOut(this)","onmousedown","CuteEditor_ButtonCommandDown(this)","onmouseup","CuteEditor_ButtonCommandUp(this)","oncontextmenu","ondragstart","PostBack","ondblclick","_ToolBarID","_CodeViewToolBarID","_FrameID"," CuteEditorFrame"," CuteEditorToolbar","cursor","no-drop","ActiveTab","Edit","Code","View","buttonInitialized","isover","CuteEditorButtonOver","CuteEditorButtonDown","CuteEditorDown","border","solid 1px #0A246A","backgroundColor","#b6bdd2","padding","1px","solid 1px #f5f5f4","inset 1px","IsCommandDisabled","CuteEditorButtonDisabled","IsCommandActive","CuteEditorButtonActive","cmd_fromfullpage","(","href","location",",DanaInfo=",",","+","scriptProperties","initfuncbecalled","GetScriptProperty","/Load.ashx?type=scripts\x26file=Gecko_Implementation","CuteEditorImplementation","function","POST","\x26getModified=1","status","GET","\x26modified=","responseText","body","block","contentEditable","InitializeCode","CuteEditorInitialize"];var _Browser_TypeInfo=null;function Browser__InitType(){if(_Browser_TypeInfo!=null){return ;} ;var Ox4={};Ox4[OxOc9ba[0]]=navigator[OxOc9ba[1]].toLowerCase();Ox4[OxOc9ba[2]]=(Ox4[OxOc9ba[0]].indexOf(OxOc9ba[3])>-1);Ox4[OxOc9ba[4]]=(Ox4[OxOc9ba[0]].indexOf(OxOc9ba[5])>-1);Ox4[OxOc9ba[6]]=(!Ox4[OxOc9ba[2]]&&Ox4[OxOc9ba[0]].indexOf(OxOc9ba[7])>-1);Ox4[OxOc9ba[8]]=(!Ox4[OxOc9ba[2]]&&Ox4[OxOc9ba[0]].indexOf(OxOc9ba[9])>-1);_Browser_TypeInfo=Ox4;} ;Browser__InitType();function Browser_IsWinIE(){return _Browser_TypeInfo[OxOc9ba[8]];} ;function Browser_IsGecko(){return _Browser_TypeInfo[OxOc9ba[6]];} ;function Browser_IsOpera(){return _Browser_TypeInfo[OxOc9ba[2]];} ;function Browser_IsSafari(){return _Browser_TypeInfo[OxOc9ba[4]];} ;function Browser_UseIESelection(){return _Browser_TypeInfo[OxOc9ba[8]];} ;function Browser_IsCSS1Compat(){return window[OxOc9ba[11]][OxOc9ba[10]]==OxOc9ba[12];} ;function CreateXMLHttpRequest(){try{if( typeof (XMLHttpRequest)!=OxOc9ba[13]){return  new XMLHttpRequest();} ;if( typeof (ActiveXObject)!=OxOc9ba[13]){return  new ActiveXObject(OxOc9ba[14]);} ;} catch(x){return null;} ;} ;function LoadXMLAsync(Oxa1d,Ox280,Ox22b,Oxa1e){var Oxe0=CreateXMLHttpRequest();function Oxa1f(){if(Oxe0[OxOc9ba[15]]!=4){return ;} ;Oxe0[OxOc9ba[16]]= new Function();var Ox288=Oxe0;Oxe0=null;if(Ox22b){Ox22b(Ox288);} ;} ;Oxe0[OxOc9ba[16]]=Oxa1f;Oxe0.open(Oxa1d,Ox280,true);Oxe0.send(Oxa1e||OxOc9ba[17]);} ;function Element_GetAllElements(p){var arr=[];if(Browser_IsWinIE()){for(var i=0;i<p[OxOc9ba[19]][OxOc9ba[18]];i++){arr.push(p[OxOc9ba[19]].item(i));} ;return arr;} ;Ox238(p);function Ox238(Ox27){var Ox239=Ox27[OxOc9ba[20]];var Ox11=Ox239[OxOc9ba[18]];for(var i=0;i<Ox11;i++){var n=Ox239.item(i);if(n[OxOc9ba[21]]!=1){continue ;} ;arr.push(n);Ox238(n);} ;} ;return arr;} ;var __ISDEBUG=false;function Debug_Todo(msg){if(!__ISDEBUG){return ;} ;throw ( new Error(msg+OxOc9ba[22]+Debug_Todo[OxOc9ba[23]]));} ;function Window_GetElement(Ox1a1,Ox93,Ox236){var Ox27=Ox1a1[OxOc9ba[11]].getElementById(Ox93);if(Ox27){return Ox27;} ;var Ox2f=Ox1a1[OxOc9ba[11]].getElementsByName(Ox93);if(Ox2f[OxOc9ba[18]]>0){return Ox2f.item(0);} ;return null;} ;function CuteEditor_AddMainMenuItems(Ox662){} ;function CuteEditor_AddDropMenuItems(Ox662,Ox669){} ;function CuteEditor_AddTagMenuItems(Ox662,Ox66b){} ;function CuteEditor_AddVerbMenuItems(Ox662,Ox66b){} ;function CuteEditor_OnInitialized(editor){} ;function CuteEditor_OnCommand(editor,Ox66f,Ox670,Ox4e){} ;function CuteEditor_OnChange(editor){} ;function CuteEditor_FilterCode(editor,Ox262){return Ox262;} ;function CuteEditor_FilterHTML(editor,Ox27a){return Ox27a;} ;function CuteEditor_FireChange(editor){window.CuteEditor_OnChange(editor);CuteEditor_FireEvent(editor,OxOc9ba[24],null);} ;function CuteEditor_FireInitialized(editor){window.CuteEditor_OnInitialized(editor);CuteEditor_FireEvent(editor,OxOc9ba[25],null);} ;function CuteEditor_FireCommand(editor,Ox66f,Ox670,Ox4e){var Ox137=window.CuteEditor_OnCommand(editor,Ox66f,Ox670,Ox4e);if(Ox137==true){return true;} ;var Ox677={};Ox677[OxOc9ba[26]]=Ox66f;Ox677[OxOc9ba[27]]=Ox670;Ox677[OxOc9ba[28]]=Ox4e;Ox677[OxOc9ba[29]]=true;CuteEditor_FireEvent(editor,OxOc9ba[30],Ox677);if(Ox677[OxOc9ba[29]]==false){return true;} ;} ;function CuteEditor_FireEvent(editor,Ox679,Ox67a){if(Ox67a==null){Ox67a={};} ;var Ox67b=editor.getAttribute(Ox679);if(Ox67b){if( typeof (Ox67b)==OxOc9ba[31]){editor[OxOc9ba[32]]= new Function(OxOc9ba[33],Ox67b);} else {editor[OxOc9ba[32]]=Ox67b;} ;editor._fireEventFunction(Ox67a);} ;} ;function CuteEditor_GetEditor(element){for(var Ox42=element;Ox42!=null;Ox42=Ox42[OxOc9ba[34]]){if(Ox42.getAttribute(OxOc9ba[35])==OxOc9ba[36]){return Ox42;} ;} ;return null;} ;function CuteEditor_DropDownCommand(element,Oxa21){var editor=CuteEditor_GetEditor(element);if(editor[OxOc9ba[37]]){return ;} ;if(element.getAttribute(OxOc9ba[38])==OxOc9ba[36]){var Ox13b=element.GetValue();if(Ox13b==OxOc9ba[39]){Ox13b=OxOc9ba[17];} ;var Ox1fa=element.GetText();if(Ox1fa==OxOc9ba[39]){Ox1fa=OxOc9ba[17];} ;element.SetSelectedIndex(0);editor.ExecCommand(Oxa21,false,Ox13b,Ox1fa);} else {if(element[OxOc9ba[40]]){var Ox13b=element[OxOc9ba[40]];if(Ox13b==OxOc9ba[39]){Ox13b=OxOc9ba[17];} ;element[OxOc9ba[41]]=0;editor.ExecCommand(Oxa21,false,Ox13b,Ox1fa);} else {element[OxOc9ba[41]]=0;} ;} ;editor.FocusDocument();} ;function CuteEditor_ExpandTreeDropDownItem(src,Ox739){var Oxb3=null;while(src!=null){if(src[OxOc9ba[42]]==OxOc9ba[43]){Oxb3=src;break ;} ;src=src[OxOc9ba[34]];} ;var Ox1dd=Oxb3[OxOc9ba[44]].item(0);Oxb3[OxOc9ba[47]][OxOc9ba[46]][OxOc9ba[45]]=OxOc9ba[17];Ox1dd[OxOc9ba[48]]=OxOc9ba[49]+Ox739+OxOc9ba[50];Oxb3[OxOc9ba[51]]= new Function(OxOc9ba[52]+Ox739+OxOc9ba[53]);} ;function CuteEditor_CollapseTreeDropDownItem(src,Ox739){var Oxb3=null;while(src!=null){if(src[OxOc9ba[42]]==OxOc9ba[43]){Oxb3=src;break ;} ;src=src[OxOc9ba[34]];} ;var Ox1dd=Oxb3[OxOc9ba[44]].item(0);Oxb3[OxOc9ba[47]][OxOc9ba[46]][OxOc9ba[45]]=OxOc9ba[54];Ox1dd[OxOc9ba[48]]=OxOc9ba[49]+Ox739+OxOc9ba[55];Oxb3[OxOc9ba[51]]= new Function(OxOc9ba[56]+Ox739+OxOc9ba[53]);} ;function Element_Contains(element,Ox17c){if(!Browser_IsOpera()){if(element[OxOc9ba[57]]){return element.contains(Ox17c);} ;} ;for(;Ox17c!=null;Ox17c=Ox17c[OxOc9ba[34]]){if(element==Ox17c){return true;} ;} ;return false;} ;function Element_SetUnselectable(element){element.setAttribute(OxOc9ba[58],OxOc9ba[59]);element.setAttribute(OxOc9ba[60],OxOc9ba[61]);var arr=Element_GetAllElements(element);var len=arr[OxOc9ba[18]];if(!len){return ;} ;for(var i=0;i<len;i++){arr[i].setAttribute(OxOc9ba[58],OxOc9ba[59]);arr[i].setAttribute(OxOc9ba[60],OxOc9ba[61]);} ;} ;function Event_GetEvent(Ox23c){Ox23c=Event_FindEvent(Ox23c);if(Ox23c==null){Debug_Todo(OxOc9ba[62]);} ;return Ox23c;} ;function Frame_GetContentWindow(Ox340){if(Ox340[OxOc9ba[63]]){return Ox340[OxOc9ba[63]];} ;if(Ox340[OxOc9ba[64]]){if(Ox340[OxOc9ba[64]][OxOc9ba[65]]){return Ox340[OxOc9ba[64]][OxOc9ba[65]];} ;} ;var Ox1a1;if(Ox340[OxOc9ba[66]]){Ox1a1=window[OxOc9ba[67]][Ox340[OxOc9ba[66]]];if(Ox1a1){return Ox1a1;} ;} ;var len=window[OxOc9ba[67]][OxOc9ba[18]];for(var i=0;i<len;i++){Ox1a1=window[OxOc9ba[67]][i];if(Ox1a1[OxOc9ba[68]]==Ox340){return Ox1a1;} ;if(Ox1a1[OxOc9ba[11]]==Ox340[OxOc9ba[64]]){return Ox1a1;} ;} ;Debug_Todo(OxOc9ba[69]);} ;function Array_IndexOf(arr,Ox23e){for(var i=0;i<arr[OxOc9ba[18]];i++){if(arr[i]==Ox23e){return i;} ;} ;return -1;} ;function Array_Contains(arr,Ox23e){return Array_IndexOf(arr,Ox23e)!=-1;} ;function Event_FindEvent(Ox23c){if(Ox23c&&Ox23c[OxOc9ba[70]]){return Ox23c;} ;if(Browser_IsGecko()){return Event_FindEvent_FindEventFromCallers();} else {if(window[OxOc9ba[33]]){return window[OxOc9ba[33]];} ;return Event_FindEvent_FindEventFromWindows();} ;return null;} ;function Event_FindEvent_FindEventFromCallers(){var Ox188=Event_GetEvent[OxOc9ba[23]];for(var i=0;i<100;i++){if(!Ox188){break ;} ;var Ox23c=Ox188[OxOc9ba[71]][0];if(Ox23c&&Ox23c[OxOc9ba[70]]){return Ox23c;} ;Ox188=Ox188[OxOc9ba[23]];} ;} ;function Event_FindEvent_FindEventFromWindows(){var arr=[];return Ox245(window);function Ox245(Ox1a1){if(Ox1a1==null){return null;} ;if(Ox1a1[OxOc9ba[33]]){return Ox1a1[OxOc9ba[33]];} ;if(Array_Contains(arr,Ox1a1)){return null;} ;arr.push(Ox1a1);var Ox246=[];if(Ox1a1[OxOc9ba[72]]!=Ox1a1){Ox246.push(Ox1a1.parent);} ;if(Ox1a1[OxOc9ba[73]]!=Ox1a1[OxOc9ba[72]]){Ox246.push(Ox1a1.top);} ;if(Ox1a1[OxOc9ba[74]]){Ox246.push(Ox1a1.opener);} ;for(var i=0;i<Ox1a1[OxOc9ba[67]][OxOc9ba[18]];i++){Ox246.push(Ox1a1[OxOc9ba[67]][i]);} ;for(var i=0;i<Ox246[OxOc9ba[18]];i++){try{var Ox23c=Ox245(Ox246[i]);if(Ox23c){return Ox23c;} ;} catch(x){} ;} ;return null;} ;} ;function include(Oxc2,Ox280){var Ox281=document.getElementsByTagName(OxOc9ba[75]).item(0);var Ox282=document.getElementById(Oxc2);if(Ox282){Ox281.removeChild(Ox282);} ;var Ox283=document.createElement(OxOc9ba[76]);Ox283.setAttribute(OxOc9ba[77],OxOc9ba[78]);Ox283.setAttribute(OxOc9ba[79],OxOc9ba[80]);Ox283.setAttribute(OxOc9ba[81],Ox280);Ox283.setAttribute(OxOc9ba[66],Oxc2);Ox281.appendChild(Ox283);} ;function Event_GetSrcElement(Ox23c){Ox23c=Event_GetEvent(Ox23c);if(Ox23c[OxOc9ba[82]]){return Ox23c[OxOc9ba[82]];} ;if(Ox23c[OxOc9ba[83]]){return Ox23c[OxOc9ba[83]];} ;Debug_Todo(OxOc9ba[84]);return null;} ;function Event_GetFromElement(Ox23c){Ox23c=Event_GetEvent(Ox23c);if(Ox23c[OxOc9ba[85]]){return Ox23c[OxOc9ba[85]];} ;if(Ox23c[OxOc9ba[86]]){return Ox23c[OxOc9ba[86]];} ;return null;} ;function Event_GetToElement(Ox23c){Ox23c=Event_GetEvent(Ox23c);if(Ox23c[OxOc9ba[87]]){return Ox23c[OxOc9ba[87]];} ;if(Ox23c[OxOc9ba[86]]){return Ox23c[OxOc9ba[86]];} ;return null;} ;function Event_GetKeyCode(Ox23c){Ox23c=Event_GetEvent(Ox23c);return Ox23c[OxOc9ba[88]];} ;function Event_GetClientX(Ox23c){Ox23c=Event_GetEvent(Ox23c);return Ox23c[OxOc9ba[89]];} ;function Event_GetClientY(Ox23c){Ox23c=Event_GetEvent(Ox23c);return Ox23c[OxOc9ba[90]];} ;function Event_GetOffsetX(Ox23c){Ox23c=Event_GetEvent(Ox23c);return Ox23c[OxOc9ba[91]];} ;function Event_GetOffsetY(Ox23c){Ox23c=Event_GetEvent(Ox23c);return Ox23c[OxOc9ba[92]];} ;function Event_IsLeftButton(Ox23c){Ox23c=Event_GetEvent(Ox23c);if(Browser_IsWinIE()){return Ox23c[OxOc9ba[93]]==1;} ;if(Browser_IsGecko()){return Ox23c[OxOc9ba[93]]==0;} ;return Ox23c[OxOc9ba[93]]==0;} ;function Event_IsCtrlKey(Ox23c){Ox23c=Event_GetEvent(Ox23c);return Ox23c[OxOc9ba[94]];} ;function Event_IsAltKey(Ox23c){Ox23c=Event_GetEvent(Ox23c);return Ox23c[OxOc9ba[95]];} ;function Event_IsShiftKey(Ox23c){Ox23c=Event_GetEvent(Ox23c);return Ox23c[OxOc9ba[96]];} ;function Event_PreventDefault(Ox23c){Ox23c=Event_GetEvent(Ox23c);Ox23c[OxOc9ba[29]]=false;if(Ox23c[OxOc9ba[70]]){Ox23c.preventDefault();} ;} ;function Event_CancelBubble(Ox23c){Ox23c=Event_GetEvent(Ox23c);Ox23c[OxOc9ba[97]]=true;if(Ox23c[OxOc9ba[98]]){Ox23c.stopPropagation();} ;return false;} ;function Event_CancelEvent(Ox23c){Ox23c=Event_GetEvent(Ox23c);Event_PreventDefault(Ox23c);return Event_CancelBubble(Ox23c);} ;function CuteEditor_BasicInitialize(editor){var Ox151=Browser_IsOpera();var Ox702= new Function(OxOc9ba[99]);var Oxa25= new Function(OxOc9ba[100]);var Oxa26= new Function(OxOc9ba[101]);var Oxa27=editor.GetScriptProperty(OxOc9ba[102]);var Oxa28=editor.GetScriptProperty(OxOc9ba[103]);var Oxa29=Oxa27+OxOc9ba[104]+Oxa28+OxOc9ba[105];var Oxa2a=Oxa27+OxOc9ba[106];var images=editor.getElementsByTagName(OxOc9ba[107]);var len=images[OxOc9ba[18]];for(var i=0;i<len;i++){var img=images[i];if(img.getAttribute(OxOc9ba[108])&&!img.getAttribute(OxOc9ba[109])){img.setAttribute(OxOc9ba[109],img.getAttribute(OxOc9ba[108]));} ;var Ox12e=img.getAttribute(OxOc9ba[110]);var Ox669=img.getAttribute(OxOc9ba[111]);if(!(Ox12e||Ox669)){continue ;} ;var Oxa2b=img.getAttribute(OxOc9ba[112]);if(parseInt(Oxa2b)>=0){img[OxOc9ba[46]][OxOc9ba[113]]=OxOc9ba[114];img[OxOc9ba[46]][OxOc9ba[115]]=OxOc9ba[114];img[OxOc9ba[81]]=Oxa2a;img[OxOc9ba[46]][OxOc9ba[116]]=OxOc9ba[117]+Oxa29+OxOc9ba[118];img[OxOc9ba[46]][OxOc9ba[119]]=OxOc9ba[120]+(Oxa2b*20)+OxOc9ba[121];img[OxOc9ba[46]][OxOc9ba[45]]=OxOc9ba[17];} ;if(!Ox12e&&!Ox669){if(Ox151){img[OxOc9ba[122]]=CuteEditor_OperaHandleImageLoaded;} ;continue ;} ;if(img[OxOc9ba[123]]!=OxOc9ba[124]){img[OxOc9ba[123]]=OxOc9ba[125];img[OxOc9ba[126]]= new Function(OxOc9ba[127]);img[OxOc9ba[128]]= new Function(OxOc9ba[129]);img[OxOc9ba[130]]= new Function(OxOc9ba[131]);img[OxOc9ba[132]]= new Function(OxOc9ba[133]);} ;if(!img[OxOc9ba[134]]){img[OxOc9ba[134]]=Event_CancelEvent;} ;if(!img[OxOc9ba[135]]){img[OxOc9ba[135]]=Event_CancelEvent;} ;if(Ox12e){var Ox188=img.getAttribute(OxOc9ba[136])==OxOc9ba[36]?Oxa25:Ox702;if(img[OxOc9ba[51]]==null){img[OxOc9ba[51]]=Ox188;} ;if(img[OxOc9ba[137]]==null){img[OxOc9ba[137]]=Ox188;} ;} else {if(Ox669){if(img[OxOc9ba[51]]==null){img[OxOc9ba[51]]=Oxa26;} ;} ;} ;} ;var Ox76f=Window_GetElement(window,editor.GetScriptProperty(OxOc9ba[138]),true);var Ox770=Window_GetElement(window,editor.GetScriptProperty(OxOc9ba[139]),true);var Ox76b=Window_GetElement(window,editor.GetScriptProperty(OxOc9ba[140]),true);Ox76b[OxOc9ba[123]]+=OxOc9ba[141];Ox76f[OxOc9ba[123]]+=OxOc9ba[142];Ox770[OxOc9ba[123]]+=OxOc9ba[142];Element_SetUnselectable(Ox76f);Element_SetUnselectable(Ox770);try{editor[OxOc9ba[46]][OxOc9ba[143]]=OxOc9ba[144];} catch(x){} ;var Ox7ef=editor.GetScriptProperty(OxOc9ba[145]);switch(Ox7ef){case OxOc9ba[146]:Ox76f[OxOc9ba[46]][OxOc9ba[45]]=OxOc9ba[17];break ;;case OxOc9ba[147]:Ox770[OxOc9ba[46]][OxOc9ba[45]]=OxOc9ba[17];break ;;case OxOc9ba[148]:break ;;} ;} ;function CuteEditor_OperaHandleImageLoaded(){var img=this;if(img[OxOc9ba[46]][OxOc9ba[45]]){img[OxOc9ba[46]][OxOc9ba[45]]=OxOc9ba[54];setTimeout(function Oxa2d(){img[OxOc9ba[46]][OxOc9ba[45]]=OxOc9ba[17];} ,1);} ;} ;function CuteEditor_ButtonOver(element){if(!element[OxOc9ba[149]]){element[OxOc9ba[134]]=Event_CancelEvent;element[OxOc9ba[128]]=CuteEditor_ButtonOut;element[OxOc9ba[130]]=CuteEditor_ButtonDown;element[OxOc9ba[132]]=CuteEditor_ButtonUp;Element_SetUnselectable(element);element[OxOc9ba[149]]=true;} ;element[OxOc9ba[150]]=true;element[OxOc9ba[123]]=OxOc9ba[151];} ;function CuteEditor_ButtonOut(){var element=this;element[OxOc9ba[123]]=OxOc9ba[125];element[OxOc9ba[150]]=false;} ;function CuteEditor_ButtonDown(){if(!Event_IsLeftButton()){return ;} ;var element=this;element[OxOc9ba[123]]=OxOc9ba[152];} ;function CuteEditor_ButtonUp(){if(!Event_IsLeftButton()){return ;} ;var element=this;if(element[OxOc9ba[150]]){element[OxOc9ba[123]]=OxOc9ba[151];} else {element[OxOc9ba[123]]=OxOc9ba[153];} ;} ;function CuteEditor_ColorPicker_ButtonOver(element){if(!element[OxOc9ba[149]]){element[OxOc9ba[134]]=Event_CancelEvent;element[OxOc9ba[128]]=CuteEditor_ColorPicker_ButtonOut;element[OxOc9ba[130]]=CuteEditor_ColorPicker_ButtonDown;Element_SetUnselectable(element);element[OxOc9ba[149]]=true;} ;element[OxOc9ba[150]]=true;element[OxOc9ba[46]][OxOc9ba[154]]=OxOc9ba[155];element[OxOc9ba[46]][OxOc9ba[156]]=OxOc9ba[157];element[OxOc9ba[46]][OxOc9ba[158]]=OxOc9ba[159];} ;function CuteEditor_ColorPicker_ButtonOut(){var element=this;element[OxOc9ba[150]]=false;element[OxOc9ba[46]][OxOc9ba[154]]=OxOc9ba[160];element[OxOc9ba[46]][OxOc9ba[156]]=OxOc9ba[17];element[OxOc9ba[46]][OxOc9ba[158]]=OxOc9ba[159];} ;function CuteEditor_ColorPicker_ButtonDown(){var element=this;element[OxOc9ba[46]][OxOc9ba[154]]=OxOc9ba[161];element[OxOc9ba[46]][OxOc9ba[156]]=OxOc9ba[17];element[OxOc9ba[46]][OxOc9ba[158]]=OxOc9ba[159];} ;function CuteEditor_ButtonCommandOver(element){element[OxOc9ba[150]]=true;if(element[OxOc9ba[162]]){element[OxOc9ba[123]]=OxOc9ba[163];} else {element[OxOc9ba[123]]=OxOc9ba[151];} ;} ;function CuteEditor_ButtonCommandOut(element){element[OxOc9ba[150]]=false;if(element[OxOc9ba[164]]){element[OxOc9ba[123]]=OxOc9ba[165];} else {if(element[OxOc9ba[162]]){element[OxOc9ba[123]]=OxOc9ba[163];} else {if(element[OxOc9ba[66]]!=OxOc9ba[166]){element[OxOc9ba[123]]=OxOc9ba[125];} ;} ;} ;} ;function CuteEditor_ButtonCommandDown(element){if(!Event_IsLeftButton()){return ;} ;element[OxOc9ba[123]]=OxOc9ba[152];} ;function CuteEditor_ButtonCommandUp(element){if(!Event_IsLeftButton()){return ;} ;if(element[OxOc9ba[162]]){element[OxOc9ba[123]]=OxOc9ba[163];return ;} ;if(element[OxOc9ba[150]]){element[OxOc9ba[123]]=OxOc9ba[151];} else {if(element[OxOc9ba[164]]){element[OxOc9ba[123]]=OxOc9ba[165];} else {element[OxOc9ba[123]]=OxOc9ba[125];} ;} ;} ;var CuteEditorGlobalFunctions=[CuteEditor_GetEditor,CuteEditor_ButtonOver,CuteEditor_ButtonOut,CuteEditor_ButtonDown,CuteEditor_ButtonUp,CuteEditor_ColorPicker_ButtonOver,CuteEditor_ColorPicker_ButtonOut,CuteEditor_ColorPicker_ButtonDown,CuteEditor_ButtonCommandOver,CuteEditor_ButtonCommandOut,CuteEditor_ButtonCommandDown,CuteEditor_ButtonCommandUp,CuteEditor_DropDownCommand,CuteEditor_ExpandTreeDropDownItem,CuteEditor_CollapseTreeDropDownItem,CuteEditor_OnInitialized,CuteEditor_OnCommand,CuteEditor_OnChange,CuteEditor_AddVerbMenuItems,CuteEditor_AddTagMenuItems,CuteEditor_AddMainMenuItems,CuteEditor_AddDropMenuItems,CuteEditor_FilterCode,CuteEditor_FilterHTML];function SetupCuteEditorGlobalFunctions(){for(var i=0;i<CuteEditorGlobalFunctions[OxOc9ba[18]];i++){var Ox188=CuteEditorGlobalFunctions[i];var name=Ox188+OxOc9ba[17];name=name.substr(8,name.indexOf(OxOc9ba[167])-8).replace(/\s/g,OxOc9ba[17]);if(!window[name]){window[name]=Ox188;} ;} ;} ;SetupCuteEditorGlobalFunctions();var __danainfo=null;var danaurl=window[OxOc9ba[169]][OxOc9ba[168]];var danapos=danaurl.indexOf(OxOc9ba[170]);if(danapos!=-1){var pluspos1=danaurl.indexOf(OxOc9ba[171],danapos+10);var pluspos2=danaurl.indexOf(OxOc9ba[172],danapos+10);if(pluspos1!=-1&&pluspos1<pluspos2){pluspos2=pluspos1;} ;__danainfo=danaurl.substring(danapos,pluspos2)+OxOc9ba[172];} ;function CuteEditor_GetScriptProperty(name){var Ox13b=this[OxOc9ba[173]][name];if(Ox13b&&__danainfo){if(name==OxOc9ba[102]){return Ox13b+__danainfo;} ;var Ox379=this[OxOc9ba[173]][OxOc9ba[102]];if(Ox13b.substr(0,Ox379.length)==Ox379){return Ox379+__danainfo+Ox13b.substring(Ox379.length);} ;} ;return Ox13b;} ;function CuteEditor_SetScriptProperty(name,Ox13b){if(Ox13b==null){this[OxOc9ba[173]][name]=null;} else {this[OxOc9ba[173]][name]=String(Ox13b);} ;} ;function CuteEditorInitialize(Oxa38,Oxa39){var editor=Window_GetElement(window,Oxa38,true);if(editor[OxOc9ba[174]]){return ;} ;editor[OxOc9ba[174]]=1;editor[OxOc9ba[173]]=Oxa39;editor[OxOc9ba[175]]=CuteEditor_GetScriptProperty;var Ox76b=Window_GetElement(window,editor.GetScriptProperty(OxOc9ba[140]),true);var editwin,editdoc;try{editwin=Frame_GetContentWindow(Ox76b);editdoc=editwin[OxOc9ba[11]];} catch(x){} ;var Oxa3a=false;var Oxa3b;var Oxa3c=false;var Oxa3d=editor.GetScriptProperty(OxOc9ba[102])+OxOc9ba[176];function Oxa3e(){if( typeof (window[OxOc9ba[177]])==OxOc9ba[178]){return ;} ;LoadXMLAsync(OxOc9ba[179],Oxa3d+OxOc9ba[180],Oxa3f);} ;function Oxa3f(Ox288){if(Ox288[OxOc9ba[181]]!=200){return ;} ;LoadXMLAsync(OxOc9ba[182],Oxa3d+OxOc9ba[183]+Ox288[OxOc9ba[184]],Oxa40);} ;function Oxa40(Ox288){if(Ox288[OxOc9ba[181]]!=200){return ;} ;CuteEditorInstallScriptCode(Ox288.responseText,OxOc9ba[177]);if(Oxa3a){Oxa41();} ;} ;function Oxa41(){if(Oxa3c){return ;} ;Oxa3c=true;try{editor[OxOc9ba[46]][OxOc9ba[143]]=OxOc9ba[17];} catch(x){} ;try{editdoc[OxOc9ba[185]][OxOc9ba[46]][OxOc9ba[143]]=OxOc9ba[17];} catch(x){} ;Ox76b[OxOc9ba[46]][OxOc9ba[45]]=OxOc9ba[186];if(Browser_IsOpera()){editdoc[OxOc9ba[185]][OxOc9ba[187]]=true;} else {} ;window.CuteEditorImplementation(editor);var Oxa42=editor.GetScriptProperty(OxOc9ba[188]);if(Oxa42){editor.Eval(Oxa42);} ;} ;function Oxa43(){if(!Element_Contains(window[OxOc9ba[11]].body,editor)){return ;} ;try{Ox76b=Window_GetElement(window,editor.GetScriptProperty(OxOc9ba[140]),true);editwin=Frame_GetContentWindow(Ox76b);editdoc=editwin[OxOc9ba[11]];var y=editdoc[OxOc9ba[185]];} catch(x){setTimeout(Oxa43,100);return ;} ;if(!editdoc[OxOc9ba[185]]){setTimeout(Oxa43,100);return ;} ;if(!Oxa3a){Oxa3a=true;setTimeout(Oxa43,50);return ;} ;if( typeof (window[OxOc9ba[177]])==OxOc9ba[178]){Oxa41();} else {try{editdoc[OxOc9ba[185]][OxOc9ba[46]][OxOc9ba[143]]=OxOc9ba[144];} catch(x){} ;} ;} ;var Oxa44=0;var Ox42=CuteEditor_Find_DisplayNone(editor);if(Ox42){function Oxa45(){if(Ox42[OxOc9ba[46]][OxOc9ba[45]]!=OxOc9ba[54]){window.clearInterval(Oxa44);Oxa44=OxOc9ba[17];editor[OxOc9ba[174]]=false;CuteEditorInitialize(Oxa38,Oxa39);} ;} ;Oxa44=setInterval(Oxa45,1000);} else {CuteEditor_BasicInitialize(editor);Oxa3e();Oxa43();} ;function CuteEditor_Find_DisplayNone(element){var Oxa47;for(var Ox42=element;Ox42!=null;Ox42=Ox42[OxOc9ba[34]]){if(Ox42[OxOc9ba[46]]&&Ox42[OxOc9ba[46]][OxOc9ba[45]]==OxOc9ba[54]){Oxa47=Ox42;break ;} ;} ;return Oxa47;} ;} ;function CuteEditorInstallScriptCode(Ox99e,Ox99f){eval(Ox99e);window[Ox99f]=eval(Ox99f);} ;window[OxOc9ba[189]]=CuteEditorInitialize;