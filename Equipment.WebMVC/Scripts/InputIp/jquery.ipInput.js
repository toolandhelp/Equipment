/*
 * jQuery Plugin: ip input box
 * Version 0.1
 *
 * Copyright (c) 2010 hjzheng (http://hi.baidu.com/hjzheng/home)
 * Licensed jointly under the GPL and MIT licenses,
 * choose which one suits your project best!
 *
 */


(function ($) {
    
    $.fn.ipInput = function(){   
        
	 return this.each(function(){
		     
        //将元素集合赋给变量 本例中是div对象 
        var $div = $(this);
		$div.addClass("ipInput-border");
		//初始化input元素			
		
		for(var i=0;i<4;i++){
            var input = $("<input type='number' maxlength='3'/>")
                .addClass("ip-address")
				.keyup(function(event){
					//获取keyCode值
					var keyCode = event.which;
					//input框
					var $input = $(this);
					//获取input框中的value
					var text = $input.val();
					//处理text中非数字字符
					$input.val(text.replace(/[^\d]/g,''));
					//防止左右键和Tab键自动跳
					if(keyCode == 39 || keyCode == 37 || keyCode == 9)  return false;	

						if(text.length >= 3){
                            if (parseInt(text) >= 256 || parseInt(text) <= 0) { 
                                MISSY.iErrorMessage("请输入0~255之间的数.");
								//alert("1111请输入0~255之间的数");
								$input.val("") 
								$input[0].focus(); 
								return false; 
                            } else {
                                try {
                                    $input.nextAll("input")[0].focus();
                                    $input[0].blur(); 
                                } catch(ex){
                                
                                }
							} 
						//输入点时 自动跳到下一个
                        } else if (text.length > 0 && (keyCode == 110 || keyCode == 190)){
								$input.nextAll("input")[0].focus();
								$input[0].blur(); 
						}
				});

			//输入IP的分割点
			$div.append(input).append($("<span>.<span>"));
		}
		//清空最后一个 .
		$div.children(":last").empty();
		
	  }); 
    }; 
})(jQuery); 


               
