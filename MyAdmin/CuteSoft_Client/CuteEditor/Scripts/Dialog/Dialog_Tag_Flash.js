var OxO7c35=["flash_preview","btnbrowse","inp_src","onclick","value","cssText","style","Movie"];var flash_preview=Window_GetElement(window,OxO7c35[0],true);var btnbrowse=Window_GetElement(window,OxO7c35[1],true);var inp_src=Window_GetElement(window,OxO7c35[2],true);btnbrowse[OxO7c35[3]]=function btnbrowse_onclick(){function Ox354(Ox137){if(Ox137){inp_src[OxO7c35[4]]=Ox137;} ;} ;editor.SetNextDialogWindow(window);editor.ShowSelectFileDialog(Ox354,inp_src.value);} ;UpdateState=function UpdateState_Flash(){flash_preview[OxO7c35[6]][OxO7c35[5]]=element[OxO7c35[6]][OxO7c35[5]];flash_preview.mergeAttributes(element);} ;SyncToView=function SyncToView_Flash(){inp_src[OxO7c35[4]]=element[OxO7c35[7]];} ;SyncTo=function SyncTo_Flash(element){element[OxO7c35[7]]=inp_src[OxO7c35[4]];} ;