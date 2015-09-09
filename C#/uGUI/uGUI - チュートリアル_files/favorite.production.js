this.JSON=JSON||{};JSON.RPC=(function(){var f="2.0";var e="application/json-rpc";var b={PARSE_ERROR:-32700,METHOD_NOT_FOUND:-32600,INVALID_REQUEST:-32601,INVALID_PARAMS:-32602,INTERNAL_ERROR:-32603};var h=function(i){return(this.isFailure()&&this.error.code==i)};var c=Class.create({initialize:function(i){this.id=i.id;this.result=i.result;this.error=i.error;this.jsonrpc=i.jsonrpc},isSuccess:function(){return(Object.isUndefined(this.error)&&!Object.isUndefined(this.result))},isFailure:function(){return !this.isSuccess()},isMethodNotFound:h.curry(b.METHOD_NOT_FOUND),isParseError:h.curry(b.PARSE_ERROR),isInvalidRequest:h.curry(b.INVALID_REQUEST),isInvalidParams:h.curry(b.INVALID_PARAMS),isInternalError:h.curry(b.INTERNAL_ERROR),isNotification:function(){return(!this.id)},getErrorCode:function(){if(this.isSuccess()){throw ("not error")}return this.error.code},isSingleResponse:Prototype.K.curry(true),isBatchResponse:Prototype.K.curry(false)});var d=Class.create({initialize:function(i){this.responseList=i;this.mapById=i.filter(function(j){return(j.id)?true:false}).inject({},function(j,k){j[k.id]=k;return j})},isSuccess:function(){return this.responseList.all(function(i){return i.isSuccess()})},get:function(i){return this.mapById[i]},isSingleResponse:Prototype.K.curry(false),isBatchResponse:Prototype.K.curry(true)});var a=function(i){if(Object.isArray(i)){return new d(i.map(a))}else{return new c(i)}};var g=Class.create({_onSuccess:function(k,j){var i=j.responseJSON||j.responseText.evalJSON();k(a(i))},_onFailure:Prototype.emptyFunction,_onNotification:Prototype.emptyFunction,initialize:function(i){this.endPoint=i;this.requests=[];this.addedId={}},add:function(i,k,l){var j=l||(this.requests.length);if(this.addedId[j]){throw ("same request id used")}this.addedId[j]=true;this.requests.push({jsonrpc:f,method:i,params:k,id:j});return this},notify:function(i,j){this.requests.push({jsonrpc:f,method:i,params:j,id:null});return this},execute:function(k){var j=Object.toJSON((this.requests.length>1)?this.requests:this.requests[0]);var i=this;this.requests=[];this.addedId={};return new Ajax.Request(this.endPoint,{method:"post",postBody:j,contentType:e,onSuccess:i._onSuccess.bind(i,k),on204:i._onNotification.bind(i,k),onFailure:i._onFailure.bind(i,k)})},call:function(i,j,k){return this.add(i,j,false).execute(k)}});g.createService=function(i,j){if(!j){return new g(i)}if(i.match(/\?/)){return new g(i+"&"+$H(j).toQueryString())}return new g(i+"?"+$H(j).toQueryString())};return{VERSION:f,Client:g}})();var Placeholder=Class.create({initialize:function(c,b){if(!Object.isElement(c)){return}this._element=$(c);var a=b||{};this._placeholder=a.placeholder||this._element.title||"input here";this._className=a.className||"defaultText";if(a.cssRule){this._cachedCSS=this._element.readAttribute("style");this._placeholderCSS=a.cssRule}this._element.observe("focus",this._focus.bindAsEventListener(this));this._element.observe("blur",this._blur.bindAsEventListener(this));Event.observe(window,"unload",this._destrcutor.bindAsEventListener(this));if(this._element.form){$(this._element.form).observe("submit",this._destrcutor.bindAsEventListener(this))}if(a.clear||this.shown()||this._element.getValue().blank()){this._showPlaceholder()}},shown:function(){return this._element.getValue()===this._placeholder},setPlaceholder:function(a){if(this.shown()){this._hidePlaceholder()}this._placeholder=a;if(!this._element.focused&&this._element.getValue().blank()){this._showPlaceholder()}},_focus:function(){if(this.shown()){this._hidePlaceholder()}},_blur:function(){if(this._element.getValue().blank()||this.shown()){this._showPlaceholder()}},_destrcutor:function(){if(this.shown()){this._element.setValue("")}},_hidePlaceholder:function(){this._element.setValue("");if(this._className){this._element.removeClassName(this._className)}if(this._placeholderCSS){this._element.writeAttribute("style",this._cachedCSS)}this._element.fire("placeholder:hide")},_showPlaceholder:function(){this._element.setValue(this._placeholder);if(this._className){this._element.addClassName(this._className)}if(this._placeholderCSS){this._element.writeAttribute("style","");this._element.setStyle(this._placeholderCSS)}this._element.fire("placeholder:show")}});(function(){var a={};Placeholder.create=function(e,d){var c=$(e).identify();if(!a[c]){a[c]=new Placeholder(e,d)}return a[c]};Placeholder.get=function(d){var c=$(d).identify();return a[c]};Placeholder.destroy=function(d){var c=$(d).identify();if(a[c]){delete a[c]}};function b(d,c){if(Object.isString(c)){c={placeholder:c.toString()}}if(d.placeholder){d.placeholder=c.placeholder}else{Placeholder.create(d,c)}}Element.addMethods(["input","textarea"],{setPlaceholder:b});document.observe("dom:loaded",function(){$$(".PLACEHOLDER").invoke("setPlaceholder")})})();(function(ownNamespace){var cache={};var MESSAGE={NOREF:":: namespace error ::not found\t",EXIST:":: namespace error ::overriding\t"};ownNamespace=export_createNamespace(ownNamespace);_.extend(ownNamespace,{createNamespace:export_createNamespace,isLoaded:export_isLoaded,depends:export_depends,using:export_using,wait:export_wait,dumpCache:export_dumpCache,"package":export_package,INCLUDE:"/static/js/"});function _truncateFQN(fqn,n){var leaves=fqn.split(".");var ret=[];for(var i=0;i<n+1;i++){ret.push(leaves[i])}return ret.join(".")}function _getNamespace(fqn){if(cache[fqn]){return cache[fqn]}try{var ns=eval("("+fqn+")");if(_canBeNamespace(ns)){return ns}throw ("")}catch(e){throw (new Error(MESSAGE.NOREF))}}function _argumentNames(func){var names=func.toString().match(/^[\s\(]*function[^(]*\(([^)]*)\)/)[1].replace(/\/\/.*?[\r\n]|\/\*(?:.|[\r\n])*?\*\//g,"").replace(/\s+/g,"").split(",");return names.length==1&&!names[0]?[]:names}function export_wait(condition,func){var cond=null;if(_.isFunction(condition)){cond=condition}if(_.isString(condition)){cond=function(){try{return _getNamespace(condition)}catch(e){return false}}}var check=function(sync){var obj=null;if(obj=cond()){if(!sync){func.apply(obj);clearInterval(id)}else{return true}}};if(check(true)){return true}var id=setInterval(check,30)}function export_dumpCache(){if(typeof console=="undefined"){return}console.dir?console.dir(cache):console.log(cache)}function _canBeNamespace(obj){return(_.isUndefined(obj)||typeof obj=="object"||(_.isFunction(obj)&&obj.prototype.initialize))?true:false}function export_createNamespace(fqn,func){if(cache[fqn]){throw (new Error(MESSAGE.EXIST+fqn))}var leaves=fqn.split(".");var tmpTop=window;var length=leaves.length;var ns=_(leaves).chain().map(function(e,i){if(_canBeNamespace(tmpTop[e])){var tmpFQN=_truncateFQN(fqn,i);if((i==length-1)&&!_.isUndefined(tmpTop[e])){throw (new Error(MESSAGE.EXIST+tmpFQN))}else{tmpTop[e]=tmpTop[e]||{__SELF_NAMESPACE:tmpFQN}}cache[tmpFQN]=tmpTop[e];tmpTop=tmpTop[e]}else{throw (new Error(MESSAGE.EXIST+e))}return tmpTop}).last().value();if(func){return func.call(ns,ns)}else{return ns}}function export_package(fqn,func){if(cache[fqn]){return func.call(cache[fqn],cache[fqn])}else{return export_createNamespace(fqn,func)}}function _getExportedObject(list,ns){var obj={};var flag=false;_(list).each(function(arg){flag=true;if(ns[arg]){obj[arg]=ns[arg]}else{throw (new Error("cant export"))}});return(flag)?obj:ns}function _applyNamespace(fqn,funcOrExport){var ns=_getNamespace(fqn);if(funcOrExport){if(_.isFunction(funcOrExport)){var func=funcOrExport;var obj=_getExportedObject(_argumentNames(func),ns);return func.apply(obj,_(_argumentNames(func)).map(function(arg){return obj[arg]}))}if(_.isArray(funcOrExport)){return _getExportedObject(funcOrExport,ns)}}else{return ns}}function export_using(fqn,func){try{return export_createNamespace(fqn,func)}catch(e){if(e.message.match(new RegExp(MESSAGE.EXIST))){return _applyNamespace(fqn,func)}}}function export_isLoaded(){try{return export_depends.apply(this,arguments)}catch(e){return false}}function export_depends(){var fqn=_(arguments).flatten();var ret=[];try{for(var i=0,l=fqn.length;i<l;i++){if(_getNamespace(fqn[i])){ret.push(_getNamespace(fqn[i]))}}}catch(e){throw new Error(MESSAGE.REQUIRE+fqn.join(",")+"/"+e.message)}if(ret.length==1){return ret[0]}else{return ret}}export_createNamespace("Mixi.Util.Namespace");Mixi.Util.Namespace.createNamespace=export_using})("Mixi.Common.Namespace");Mixi.Common.Namespace.createNamespace("Mixi.Common.String",function(i){this.tuneHtml=function f(m){if(Object.isNumber(m)){m=m.toString()}if(!Object.isString(m)){return""}m=c(m);return m.replace(/</g,"&lt;").replace(/>/g,"&gt;").replace(/\n/g,"<br />").replace(/\{/g,"&#123;").replace(/\}/g,"&#125;").replace(/\+/g,"&#043;")};this.tuneForm=function b(m){if(Object.isNumber(m)){m=m.toString()}if(!Object.isString(m)){return""}return m.replace(/</g,"&lt;").replace(/>/g,"&gt;").replace(/\{/g,"&#123;").replace(/\}/g,"&#125;").replace(/\+/g,"&#043;").replace(/'/g,"&#039;").replace(/"/g,"&quot;")};this.tuneTextarea=function d(m){if(Object.isNumber(m)){m=m.toString()}if(!Object.isString(m)){return""}return m.replace(/</g,"&lt;").replace(/>/g,"&gt;").replace(/\{/g,"&#123;").replace(/\}/g,"&#125;").replace(/\+/g,"&#043;")};this.escapeHTML=function(m){if(_.isNumber(m)){m=m.toString()}if(!_.isString(m)){return""}return m.replace(/</g,"&lt;").replace(/>/g,"&gt;").replace(/\{/g,"&#123;").replace(/\}/g,"&#125;").replace(/\+/g,"&#043;").replace(/'/g,"&#039;").replace(/"/g,"&quot;")};this.lineFeedToBreak=function(m){if(_.isNumber(m)){m=m.toString()}if(!_.isString(m)){return""}return m.replace(/\n/g,"<br />")};var j=function(m){if(m<10){return"0"+m.toString()}return m.toString()};this.formatTime=function k(n){var m=new Date(n);return j(m.getHours())+":"+j(m.getMinutes())};this.formatDate=function l(n){var m=new Date(n);return(m.getMonth()+1)+"\u6708"+(m.getDate())+"\u65E5"};this.formatDatetime=function e(p,q){var o=(new Date());var m=new Date();m.setTime(p);var n=m.getFullYear()+"\u5e74";n+=(q?j((m.getMonth()+1)):(m.getMonth()+1))+"\u6708";n+=(q?j(m.getDate()):m.getDate())+"\u65e5";n+=" "+this.formatTime(p);return n};this.foldText=function h(r,m,s){if(m<=0){return r}if(Object.isNumber(r)){r=r.toString()}if(!Object.isString(r)){return""}if(r.length==0){return""}var w=m*2;var p=escape(r);var v=p.length;var s=s||"��";var q=0;var o=0;var n=0;var u=[];while(q<v&&n<w){if(p.charAt(q)=="%"){if(p.charAt(q+1)=="u"){n+=2;q+=6}else{n++;q+=3}}else{n++;q++}u.push(r.charAt(o++))}var t=u.join("")+(q<v?s:"");return t};this.breakUrl=function a(n){if(Object.isNumber(n)){n=text.toString()}if(!Object.isString(n)){return""}for(var o=0,m="";o<n.length;o++){m+=n.charAt(o);if(o<n.length-1&&((o+1)%7)==0){m+="<wbr/>"}}return m};this.breakWord=function g(r,o){var q=r.length;var p=r.split("");var m=0;var n=[];while(m<q){n.push(this.foldText(r.substring(m),o));m+=o}return n.join("\n")};this.countByteLength=function(m){if(!Object.isString(m)){return 0}return m.split("").inject(0,function(n,o){return n+(escape(o).substring(0,2)=="%u"?2:1)})};this.stripCRLF=function(m){if(!Object.isString(m)){return""}return m.replace(/[\r|\n]/g,"")};this.countByteLengthFast=function(p){var n=escape(p);var q=n.length;var m=0;var o=0;while(m<q){if(n.charAt(m)=="%"){if(n.charAt(m+1)=="u"){m=m+6;o=o+2}else{m=m+3;o++}}else{o++;m++}}return o};this.containsEmoji=function(m){if(!m){return false}return/\[[a-z]:\d+\]/.test(m)};this.commify=function(n){var m=/((?:^|[^.0-9])[-+]?\d+)(\d{3})/;n+="";while(m.test(n)){n=n.replace(m,"$1,$2")}return n};var c=function c(n){if(!Object.isString(n)){return""}var m="[\\w\\-.?&,/=:%+~;#]*";return n.replace(new RegExp("(https?)://(m\\.)?(mixi\\.jp/)(\\w+)(\\.pl)\\?("+m+")(ses=\\w+&?)("+m+")","mg"),"$1://$2$3$4$5?$6$8")};this.isBlank=function(m){return/^(\s|\u3000)*$/.test(m)}});Mixi.Common.Namespace.using("Mixi.Validator",function(){var a=this;var b=/[^\u0000-\u007f]+/g;a.MaxLength=function(d,g){var f=g;if(Object.isFunction(g)){f=g()}f=f.replace(/\r?\n/g,"\r\n");var e=f.length;e+=(f.match(b)||[]).join("").length;var h=Math.ceil((e-(d*2))/2);var c=[];if(h>0){c.push(h)}return c}});Mixi.Common.Namespace["package"]("Mixi.Plugins.Favorite",function(ns){var __ie_version=false;
/*@cc_on __ie_version = @_jscript_version; @*/
var SHOW_COMMENT_FORM_DELAY=0.3;var HIDE_COMMENT_FORM_DELAY_SHORT=0.1;var HIDE_COMMENT_FORM_DELAY_LONG=0.8;var HIDE_COMMENT_FORM_DELAY_AFTER_MESSAGE=1.5;var SHOW_COMMENT_FORM_AFTER_LIKE_DELAY=0.8;var SHOW_NAME_TOOLTIP_DELAY=0.35;var AFTER_UPDATE_EFFECT_QUEUE="after_update";var COMPONENT_EFFECT_QUEUE="component";var HIDE_MESSAGE_BOX_DELAY=1;var BALLOON_TITLE_MARGIN_BOTTOM=10;var JsonRpc=Mixi.Common.Namespace.depends("JSON.RPC");var Validator=Mixi.Common.Namespace.depends("Mixi.Validator");var Placeholder=Mixi.Common.Namespace.depends("Placeholder");var StringUtils=Mixi.Common.Namespace.depends("Mixi.Common.String");Object.extend(ns,{createRpcClient:function(params){return JsonRpc.Client.createService("/system/rpc.json",params)},createRpcClientWithAuthSecret:function(secret){return ns.createRpcClient({auth_type:"postkey",secret:secret})}});ns.Button=Class.create({initialize:function(){this.options={};document.observe("plugins:login_succeess",function(event){this.options.secret=event.memo.secret;this.onLoginSuccess.bind(this).defer()}.bindAsEventListener(this))},showCreateFavoriteButton:function(){var controls=this.options.controls;controls.createButton.show();controls.deleteButton.hide()},showErrorMessage:function(){var controls=this.options.controls;this.options.secret=null;controls.createButton.hide();controls.deleteButton.hide();controls.errorMessage.show()},disableDeleteFavoriteButtonUntilMouseLeave:function(){var deleteButton=this.options.controls.deleteButton;var deleteLink=deleteButton.down(".undo a");var deleteFavoriteElement=this.getDeleteFavoriteButton();this.resetDeleteButton();deleteFavoriteElement.observe("click",function(event){event.stop()});var timeoutId=null;deleteButton.observe("mouseover",function(event){if(timeoutId!=null){clearTimeout(timeoutId)}});deleteButton.observe("mouseout",function(event){timeoutId=this.configureDeleteFavoriteButton.bind(this).delay(0.3)}.bind(this))},showDeleteFavoriteButton:function(){var createButton=this.options.controls.createButton;var deleteButton=this.options.controls.deleteButton;createButton.hide();if(this.options.controls.membersList){deleteButton.show()}else{new Effect.Appear(deleteButton,{duration:0.4,queue:{scope:AFTER_UPDATE_EFFECT_QUEUE,position:"front"}})}if(this.options.commentEnabled){this.showCommentForm(true);this._setupDeleteButtonMouseEvent()}},deleteButtonCreateShowCommentTimeout:function(){var membersList=this.options.controls.membersList;this._showCommentTimeoutId=function(){ns.commentFrameProxy.addCommentShowedHandler(function(commentForm){ns.commentFrameProxy.installHideCommentFormAction({delay:HIDE_COMMENT_FORM_DELAY_AFTER_MESSAGE})},true);this.showCommentForm();if(membersList){membersList.installHideNameTooltipAction()}}.bind(this).delay(0.2)},deleteButtonClearShowCommentTimeout:function(){if(this._showCommentTimeoutId!=null){clearTimeout(this._showCommentTimeoutId)}},_resetDeleteButtonMouseEvent:function(){var deleteButton=this.options.controls.deleteButton.down(".favoriteButton")||this.options.controls.deleteButton;deleteButton.stopObserving("mouseover",this._deleteButtonCreateShowCommentTimeout);deleteButton.stopObserving("mouseout",this._deleteButtonClearShowCommentTimeout)},_setupDeleteButtonMouseEvent:function(){var deleteButton=this.options.controls.deleteButton.down(".favoriteButton")||this.options.controls.deleteButton;this._resetDeleteButtonMouseEvent();deleteButton.observe("mouseover",this._deleteButtonCreateShowCommentTimeout);deleteButton.observe("mouseout",this._deleteButtonClearShowCommentTimeout)},_callUpdateFavoriteProcedure:function(proc_name,callback){var client=ns.createRpcClientWithAuthSecret(this.options.secret);client.call(proc_name,{uri:this.options.uri,service_key:this.options.key,via:this.options.touchDevice?"smallscreen":"pc"},function(transport){if(transport.error){this.showErrorMessage();return}if(!transport.result){return}if(transport.result.error){return}callback(transport)}.bind(this))},createFavorite:function(options){this._callUpdateFavoriteProcedure("jp.mixi.plugins.favorite.createFavorite",function(transport){this._update(transport.result);this.showDeleteFavoriteButton();options.onSuccess&&options.onSuccess(transport)}.bind(this))},deleteFavorite:function(options){this._callUpdateFavoriteProcedure("jp.mixi.plugins.favorite.deleteFavorite",function(transport){this.showCreateFavoriteButton();this._update(transport.result);if(this.options.commentEnabled){ns.commentFrameProxy.installHideCommentFormAction({delay:HIDE_COMMENT_FORM_DELAY_SHORT})}options.onSuccess&&options.onSuccess(transport)}.bind(this))},synchronizeCountBalloon:function(count){var controls=this.options.controls;var _this=this;[controls.createButton,controls.deleteButton].each(function(button){button.select(".count").each(function(element){element.down("strong").update(_this._formatCountString(count));element[count?"show":"hide"]()})});controls.createButton.down("a")[count?"removeClassName":"addClassName"]("simple")},_formatCountString:function(count){if(!count){return""}if(count>99999){return Math.floor(count/1000)+"K"}else{return StringUtils.commify(count)}},_update:function(result){var _this=this;var controls=this.options.controls;var members=result.members;if(this.options.countEnabled){this.synchronizeCountBalloon(result.all_members_count)}if(controls.membersList){controls.membersList.update(members)}},onLoginSuccess:function(){window.top.focus();if(this.options.secret){this.createFavorite()}},openLoginWindow:function(){window.open(this.options.login,"login_popup",["width=632","height=456","location=yes","resizable=yes","toolbar=no","menubar=no","scrollbars=no","status=no"].join(","))},configureCreateFavoriteButton:function(element){element.observe("click",function(event){var doDisable=!this.options.touchDevice;element.observe("mouseout",function(){doDisable=false});if(this.options.secret){this.createFavorite({onSuccess:function(transport){if(doDisable){this.disableDeleteFavoriteButtonUntilMouseLeave()}element.stopObserving("mouseout")}.bind(this)})}else{this.openLoginWindow()}event.stop()}.bindAsEventListener(this));if(this.options.touchDevice){element.setStyle({backgroundPosition:"0 0"})}},getDeleteFavoriteButton:function(){var deleteButton=this.options.controls.deleteButton;return this.options.oneClickDeleteEnabled?deleteButton:deleteButton.down(".undo a")},resetDeleteButton:function(){var deleteButton=this.options.controls.deleteButton;var deleteLink=deleteButton.down(".undo a");var deleteFavoriteElement=this.getDeleteFavoriteButton();deleteFavoriteElement.stopObserving("mouseover");deleteFavoriteElement.stopObserving("mouseout");deleteLink.stopObserving("mouseover",this._deleteLinkAddClassName);deleteLink.stopObserving("mouseout",this._deleteLinkremoveClassName);deleteLink.stopObserving("click")},configureDeleteFavoriteButton:function(){var createButton=this.options.controls.createButton;var deleteButton=this.options.controls.deleteButton;var deleteLink=deleteButton.down(".undo a");this.resetDeleteButton();var deleteFavoriteElement=this.getDeleteFavoriteButton();deleteFavoriteElement.observe("click",function(event){if(this.options.commentEnabled){ns.commentFrameProxy.hideComment()}this.options.secret?this.deleteFavorite():this.openLoginWindow();event.stop()}.bindAsEventListener(this));deleteLink.observe("mouseover",this._deleteLinkAddClassName);deleteLink.observe("mouseout",this._deleteLinkremoveClassName);if(this.options.commentEnabled){if(deleteButton.visible()){this._setupDeleteButtonMouseEvent()}ns.commentFrameProxy.addCommentSuccessHandler(function(){this._resetDeleteButtonMouseEvent()}.bind(this))}},showCommentForm:function(setFocus){var membersList=this.options.controls.membersList;if(this.options.commentEnabled&&this.options.secret){ns.commentFrameProxy.showComment(null,function(commentForm){if(membersList){membersList.installHideNameTooltipAction()}if(setFocus){commentForm.setFocus()}})}},redirectWhenAuthTicketBroken:function(){var queries=$H((location.search||"").toQueryParams());if(queries.get("openid.user_setup_url")){var client=ns.createRpcClient({auth_type:"none"});client.call("jp.mixi.plugins.hasAuth",{},function(transport){if(!transport.result){return}queries.keys().each(function(k){if(k.match(/^openid\./)){queries.unset(k)}});location.href=(location.pathname+"?"+queries.toQueryString())}.bind(this))}},configure:function(options){this.options=Object.extend({touchDevice:false,secret:null},options||{});var deleteButton=this.options.controls.deleteButton;var deleteLink=deleteButton.down(".undo a");this._deleteButtonCreateShowCommentTimeout=this.deleteButtonCreateShowCommentTimeout.bind(this);this._deleteButtonClearShowCommentTimeout=this.deleteButtonClearShowCommentTimeout.bind(this);this._deleteLinkAddClassName=deleteLink.addClassName.bind(deleteLink,"undoIcon");this._deleteLinkremoveClassName=deleteLink.removeClassName.bind(deleteLink,"undoIcon");if(!this.options.secret){this.redirectWhenAuthTicketBroken()}this.options.oneClickDeleteEnabled=this.options.touchDevice;if(this.options.commentEnabled){this.options.commentEnabled=!this.options.touchDevice}if(this.options.commentEnabled){ns.commentFrameProxy=new ns.CommentFrameProxy();var commentInitHandler=null;if(this.options.controls.comment){commentInitHandler=function(){ns.commentFrameProxy.onReady(this.options.controls.comment)}.bind(this)}else{commentInitHandler=function(){__MIXI_PLUGINS__.Proxy.postMessage({act:"create"})}}ns.commentFrameProxy.addCommentOptionsHandler(function(commentForm){$w("uri key secret").each(function(name){commentForm.options[name]=this.options[name]}.bind(this))}.bind(this));ns.commentFrameProxy.setCommentInitializeHandler(commentInitHandler)}if(this.options.countEnabled){this.synchronizeCountBalloon(this.options.allMembersCount)}this.configureCreateFavoriteButton(this.options.controls.createButton.down(".favoriteButton a")||this.options.controls.createButton.down(".createFavoriteButton a"));this.configureDeleteFavoriteButton();return this}});ns.CommentFrameProxy=Class.create({_callbacks:{ready:[],success:[],show:[],options:[]},_commentform:null,_commentInitHandler:null,_initialized:false,setCommentInitializeHandler:function(handler){this._commentInitHandler=handler},ready:function(callback){if(!this._commentform){this._callbacks.ready.push({callback:callback,once:true})}else{callback(this._commentform)}},addCommentSuccessHandler:function(callback,once){this._callbacks.success.push({callback:callback,once:!!once})},addCommentShowedHandler:function(callback,once){this._callbacks.show.push({callback:callback,once:!!once})},addCommentOptionsHandler:function(callback,once){this._callbacks.options.push({callback:callback,once:!!once})},_processCallbacks:function(_callbacks){return _callbacks.findAll(function(callbackObj){callbackObj.callback(this._commentform);return !callbackObj.once}.bind(this))},onCommentSuccess:function(){this._callbacks.success=this._processCallbacks(this._callbacks.success)},onCommentOptions:function(){this._callbacks.options=this._processCallbacks(this._callbacks.options)},onCommentShow:function(){this._callbacks.show=this._processCallbacks(this._callbacks.show)},onReady:function(commentForm){this._commentform=commentForm;if(!commentForm){return false}this._callbacks.ready=this._processCallbacks(this._callbacks.ready)},_initialize:function(){if(!this._initialized&&this._commentInitHandler){this._initialized=true;this._commentInitHandler()}},showComment:function(before,after){this._initialize();if(after){this.addCommentShowedHandler(function(commentForm){after(commentForm)},true)}this.ready(function(commentForm){if(before){before(commentForm)}commentForm.show()})},installHideCommentFormAction:function(options){this._initialize();this.ready(function(commentForm){commentForm.installHideCommentFormAction(options)})},hideComment:function(){this._initialize();this.ready(function(commentForm){commentForm.hide()})}});ns.CommentForm=Class.create({initialize:function(element){this.element=element;this.options={}},_show:function(){this.element.show();this._fireCommentShowEvent()},_fireCommentShowEvent:function(){ns.commentFrameProxy.onCommentShow()},_hide:function(){this.element.hide()},_visible:function(){return this.element.visible()},_onCommentSuccess:function(){ns.commentFrameProxy.onCommentSuccess()},_makeSureOptions:function(){ns.commentFrameProxy.onCommentOptions()},show:function(){this.clearHideCommentFormAction();if(!this._showCommentFormTimeoutId&&!this._visible()){this._showCommentFormTimeoutId=(function(){this.validateAndWarnMaxLength();this.hideCommentCompleteMessage();this._show()}).bind(this).delay(SHOW_COMMENT_FORM_DELAY)}else{this._fireCommentShowEvent()}},setFocus:function(){this.getCommentInput().activate()},hide:function(){if(this._showCommentFormTimeoutId){clearTimeout(this._showCommentFormTimeoutId);this._showCommentFormTimeoutId=null}this._hide();this.getCommentInput().blur();this.clearHideCommentFormAction()},installHideCommentFormAction:function(options,callback){var seconds=options.delay||HIDE_COMMENT_FORM_DELAY_SHORT;this._hideCommentFormTimeoutId=function(){if(!this._focused&&!this._mousein){this.hide();if(callback){callback()}}}.bind(this).delay(seconds)},clearHideCommentFormAction:function(){if(this._hideCommentFormTimeoutId!=null){clearTimeout(this._hideCommentFormTimeoutId);this._hideCommentFormTimeoutId=null}},hideCommentCompleteMessage:function(){var messageElement=this.getMessageElement();var contentsElement=this.element.down(".contents");messageElement.hide().update();contentsElement.select("input").invoke("setStyle",{visibility:"visible"})},showCommentCompletedMessage:function(message){var messageElement=this.getMessageElement();var contentsElement=this.element.down(".contents");var dimensions=contentsElement.getDimensions();dimensions.width-=14;dimensions.height-=4;messageElement.absolutize();messageElement.setStyle({top:"0px",left:"2px",width:dimensions.width+"px",height:dimensions.height+"px",lineHeight:dimensions.height+"px",backgroundColor:"white"});contentsElement.select("input").invoke("setStyle",{visibility:"hidden"});messageElement.show();messageElement.update(StringUtils.tuneHtml(message))},onCommentFormSubmit:function(event){if(this.validateAndWarnMaxLength()){this._stopMouseEvent();this.clearHideCommentFormAction();this._makeSureOptions();var client=ns.createRpcClientWithAuthSecret(this.options.secret);client.call("jp.mixi.plugins.favorite.addComment",{uri:this.options.uri,service_key:this.options.key,comment:this.getCommentInput().getValue(),via:this.options.touchDevice?"smallscreen":"pc"},function(transport){if(!transport.result){this.showCommentCompletedMessage("コメントできませんでした")}else{this.showCommentCompletedMessage("コメントしました");this.clear();this._onCommentSuccess()}this.clearHideCommentFormAction();this.installHideCommentFormAction({delay:HIDE_COMMENT_FORM_DELAY_AFTER_MESSAGE},this._setupMouseEvent.bind(this))}.bind(this))}event.stop()},_setupMouseEvent:function(){var _this=this;var control=this.element;this._stopMouseEvent();control.observe("mouseover",function(event){_this._mousein=true;_this.clearHideCommentFormAction()});control.observe("mouseout",function(event){_this._mousein=false;_this.installHideCommentFormAction({delay:HIDE_COMMENT_FORM_DELAY_LONG})})},_stopMouseEvent:function(){var control=this.element;control.stopObserving("mouseover");control.stopObserving("mouseout")},configureCommentForm:function(){var _this=this;var control=this.element;var submitButton=this.getSubmitButton();this._setupMouseEvent();var commentInput=this.getCommentInput();commentInput.observe("keyup",this.onCommentFormKeyUp.bindAsEventListener(this));commentInput.observe("keydown",this.onCommentFormKeyDown.bindAsEventListener(this));commentInput.observe("focus",function(event){_this._focused=true});commentInput.observe("blur",function(event){_this._focused=false;_this.installHideCommentFormAction({delay:HIDE_COMMENT_FORM_DELAY_LONG})});var form=control.down("form");if(__ie_version&&__ie_version>8){form.addEventListener=false}form.observe("submit",this.onCommentFormSubmit.bindAsEventListener(this));submitButton.observe("click",function(event){if(!_this.validateAndWarnMaxLength()){event.stop()}});var closeContainer=control.down(".close");if(closeContainer){var closeButton=closeContainer.down("a");if(closeButton){closeButton.observe("click",function(event){_this.hide();event.stop()})}}document.observe("click",function(event){if(_this._visible()&&!event.element().descendantOf(control)){_this.installHideCommentFormAction({delay:HIDE_COMMENT_FORM_DELAY_SHORT})}});commentInput.observe("placeholder:hide",function(){commentInput.activate()});this.clear()},clear:function(){var input=this.getCommentInput();input.setValue("");input.setPlaceholder(input.getAttribute("data-placeholder")||"");input.blur();this._mousein=false},getCommentInput:function(){return this.element.down(".comment")},getAlertElement:function(){return this.element.down(".alert")},getSubmitButton:function(){return this.element.down("input[type=image]")},getMessageElement:function(){return this.element.down(".message")},validateMaxLength:function(){var result=Validator.MaxLength(this.options.maxCharLength,this.getCommentInput().getValue());return result.length>0?result[0]:0},validateAndWarnMaxLength:function(){if(this.isEmpty()){this.disable();return false}var input=this.getCommentInput();var error=this.getAlertElement();var over=this.validateMaxLength();if(over>0){error.down("span.numCharacters").update(over+"");error.show();this.disable();return false}else{error.hide();this.enable();return true}},isEmpty:function(){var input=this.getCommentInput();var placeholder=Placeholder.get(input);return placeholder.shown()||!input.present()},enable:function(){var button=this.getSubmitButton();button.enable();button.setOpacity(1);button.setStyle({cursor:"pointer"})},disable:function(){var button=this.getSubmitButton();button.setOpacity(0.6);button.setStyle({cursor:"default"})},onCommentFormKeyDown:function(event){var code=Object.isUndefined(event.which)?event.keyCode:event.which;if(code==Event.KEY_RETURN){event.stop()}},onCommentFormKeyUp:function(event){this.validateAndWarnMaxLength();this.getCommentInput().focus()},configure:function(options){this.options=Object.extend({touchDevice:false,maxCharLength:150},options||{});this.configureCommentForm();return this}});ns.CommentFrame=Class.create(ns.CommentForm,{_visibility:false,_favoriteNamespace:null,_favoriteWindow:null,initialize:function($super,element){$super(element);var hash=__MIXI_PLUGINS__.Encoder.hash();if(hash){var param=__MIXI_PLUGINS__.Encoder.decode(hash);if(param&&param.ftcb){var favoriteWindow=parent.frames[param.ftcb];this._favoriteWindow=favoriteWindow;if(favoriteWindow){this._favoriteNamespace=favoriteWindow.Mixi.Common.Namespace.depends("Mixi.Plugins.Favorite");var dimensions=element.getDimensions();this._favoriteWindow.__MIXI_PLUGINS__.Proxy.postMessage({act:"size_inited",height:(dimensions.height)});this._favoriteNamespace.commentFrameProxy.onReady(this)}}}},hideCommentCompleteMessage:function(){var messageElement=this.getMessageElement();var contentsElement=this.element.down(".contents");messageElement.hide().update();contentsElement.down(".balloonTitle").show();contentsElement.down("form").show()},showCommentCompletedMessage:function(message){var messageElement=this.getMessageElement();var contentsElement=this.element.down(".contents");var formDimensions=contentsElement.down("form").getDimensions();var titleDimensions=contentsElement.down(".balloonTitle").getDimensions();titleDimensions.height-=BALLOON_TITLE_MARGIN_BOTTOM;messageElement.setStyle({width:(formDimensions.width)+"px",height:(formDimensions.height+titleDimensions.height)+"px",lineHeight:(formDimensions.height)+"px",backgroundColor:"white"});contentsElement.down(".balloonTitle").hide();contentsElement.down("form").hide();messageElement.show();messageElement.update(StringUtils.tuneHtml(message))},onCommentFormKeyDown:function(event){},_callProxyUtilShowed:function(){if((document.body.offsetWidth||document.documentElement.offsetWidth)>10){this._visibility=true;var _doCommentShow=function(){this._fireCommentShowEvent()}.bind(this).delay(SHOW_COMMENT_FORM_DELAY)}else{this._callProxyUtilShowed.bind(this).delay(0.1)}},_fireCommentShowEvent:function(){this._favoriteNamespace.commentFrameProxy.onCommentShow()},_show:function(){if(!this._favoriteNamespace){return}this._favoriteWindow.__MIXI_PLUGINS__.Proxy.postMessage({act:"show"});this._callProxyUtilShowed.bind(this).delay(0.1)},_hide:function(){if(!this._favoriteNamespace){return}this._favoriteWindow.__MIXI_PLUGINS__.Proxy.postMessage({act:"hide"});this._visibility=false},_visible:function(){return this._visibility},_makeSureOptions:function(){this._favoriteNamespace.commentFrameProxy.onCommentOptions()},_onCommentSuccess:function(){if(!this._favoriteNamespace){return}this._favoriteNamespace.commentFrameProxy.onCommentSuccess()}});ns.MembersList=Class.create({initialize:function(element){this.element=element;this.options={}},configureNameTooltip:function(element){if(!this.options.nameTooltipEnabled){return}element.observe("mouseover",this.installHideNameTooltipAction.bind(this))},updateNameTooltipContentWithMemberListItemElement:function(li){if(!this.options.nameTooltipEnabled){return}var nameElement=this.options.nameElement;var linkElement=li.down("a");nameElement.down("span").update(linkElement.innerHTML)},showNameTooltipOverMemberListItemElement:function(li){if(!this.options.nameTooltipEnabled){return}var nameElement=this.options.nameElement;nameElement.setStyle({visibility:"hidden",left:"0px"});this.updateNameTooltipContentWithMemberListItemElement(li);var maxWidth=this.element.getWidth();var dimensions=nameElement.getDimensions();nameElement.clonePosition(li,{setTop:false,setLeft:true,setWidth:false,setHeight:false});var x=parseInt(nameElement.getStyle("left"),10);var maxX=x+dimensions.width;var style={visibility:"visible",top:-dimensions.height+"px"};if(maxX>maxWidth){style.left=(x-(maxX-maxWidth))+"px"}nameElement.setStyle(style);if(!this._showNameTooltipEffect&&!nameElement.visible()){this._showNameTooltipEffect=new Effect.Appear(nameElement,{duration:0.2,delay:SHOW_NAME_TOOLTIP_DELAY,queue:{scope:COMPONENT_EFFECT_QUEUE,position:"end"},afterFinish:function(effect){this._showNameTooltipEffect=null}.bind(this)})}},installHideNameTooltipAction:function(){if(!this.options.nameTooltipEnabled){return}var nameElement=this.options.nameElement;this._hideNameElementTimeoutId=function(){if(this._showNameTooltipEffect){this._showNameTooltipEffect.cancel();this._showNameTooltipEffect=null}nameElement.hide()}.bind(this).delay(0.1)},clearHideNameTooltipAction:function(){if(!this.options.nameTooltipEnabled){return}if(this._hideNameElementTimeoutId!=null){clearTimeout(this._hideNameElementTimeoutId)}},configureMemberListItemElement:function(element){var nameElement=this.options.nameElement;if(this.options.nameTooltipEnabled){element.observe("mouseover",function(event){this.clearHideNameTooltipAction();this.showNameTooltipOverMemberListItemElement(element)}.bind(this));element.observe("mouseout",this.installHideNameTooltipAction.bind(this))}return element},removeMemberListItemElement:function(element){element.stopObserving();element.remove()},createMemberIconElement:function(member){return new Element("a",{href:member.content_url,target:"_blank"}).update(StringUtils.tuneHtml(member.nickname)).setStyle({backgroundImage:"url("+StringUtils.tuneHtml(member.thumbnail_url)+")"})},createMemberListItemElement:function(member){var element=new Element("li",{"data-member-id":member.member_id}).update(this.createMemberIconElement(member));return this.configureMemberListItemElement(element)},updateMembersListImmediately:function(members){this.element.select(".friendListWrapper").invoke("remove");var wrapper=new Element("div",{className:"friendListWrapper"});var friendList=new Element("ul",{className:"friendList"});members.each(function(member){friendList.insert(this.createMemberListItemElement(member))}.bind(this));this.element.insert({top:wrapper});wrapper.insert({top:friendList})},update:function(members){members=(members.length>this.options.maxVisibleMembers)?members.slice(0,this.options.maxVisibleMembers):members.clone();var friendList=this.element.down(".friendList");if(!friendList){this.updateMembersListImmediately(members);return}var friendListItems=friendList.select("li");var friendListItemByMemberId=friendListItems.inject({},function(accum,li){var memberId=li.getAttribute("data-member-id");accum[memberId]=li;return accum});var items=members.map(function(m){return{member:m,memberId:m.member_id,visible:true,animation:"insert",element:undefined}});var sibling=null;items.reverse(false).each(function(item){var li=friendListItemByMemberId[item.memberId];if(li){item.animation="none";item.element=li;sibling=li;delete friendListItemByMemberId[item.memberId]}else{var element=this.createMemberListItemElement(item.member);item.animation="insert";item.element=element;element.hide();sibling!=null?sibling.insert({before:element}):friendList.insert({bottom:element})}}.bind(this));var animation=(items.length>=this.options.maxVisibleMembers)?"slideout":"remove";friendListItems.reverse(false).each(function(listItem){var memberId=listItem.getAttribute("data-member-id");var li=friendListItemByMemberId[memberId];if(!li){animation="remove"}else{items.push({memberId:memberId,visible:false,animation:animation,element:li})}});items.each(function(item){if(item.animation=="insert"){this.showMemberFace(item.element)}else{if(item.animation=="remove"){this.removeMemberFace(item.element)}}}.bind(this))},showMemberFace:function(element){new Effect.Morph(element,{style:"width:"+this.options.faceIconWidth+"px",duration:0.2,queue:{scope:AFTER_UPDATE_EFFECT_QUEUE,position:"end"},beforeStart:function(effect){effect.element.setStyle({width:"0px"})},afterSetup:function(effect){var element=effect.element;element.show();element.setOpacity(0)}});new Effect.Appear(element,{duration:0.4,queue:{scope:AFTER_UPDATE_EFFECT_QUEUE,position:"end"}})},removeMemberFace:function(element){new Effect.Morph(element,{style:"width: 0px",duration:0.2,queue:{scope:AFTER_UPDATE_EFFECT_QUEUE,position:"end"},beforeStart:function(effect){effect.element.makeClipping()},afterFinish:function(effect){this.removeMemberListItemElement(effect.element)}.bind(this)})},configure:function(options){this.options=Object.extend({nameElement:null,touchDevice:false,showCount:false,showFaces:"true",size:undefined},options||{});try{var config=__MIXI_PLUGINS__.FavoriteUtil.createFavoriteConfig(this.options.size,this.options.showCount,this.options.showFaces,this.options.frameWidth);this.options.maxVisibleMembers=config.faces.max;this.options.faceIconWidth=config.face.width}catch(err){var FRAME_MIN_WIDTH=120;var FRAME_DEFAULT_WIDTH=450;var PROFILE_ICON_SPACING=5;var PROFILE_ICON_WIDTH=40;if(!this.options.frameWidth){this.options.frameWidth=FRAME_DEFAULT_WIDTH}else{if(this.options.frameWidth<FRAME_MIN_WIDTH){this.options.frameWidth=FRAME_MIN_WIDTH}}this.options.faceIconWidth=PROFILE_ICON_WIDTH;this.options.maxVisibleMembers=Math.floor(this.options.frameWidth/(PROFILE_ICON_WIDTH+PROFILE_ICON_SPACING))}this.options.nameTooltipEnabled=!this.options.touchDevice;this.configureNameTooltip(this.options.nameElement);this.element.select(".friendList li").each(function(li){this.configureMemberListItemElement(li)}.bind(this));return this}})});