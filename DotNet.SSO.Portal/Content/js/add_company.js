$(function() {
	if($("#uploader-img").length>0) up_zhengjian();
	$(".selectlb").click(function(){
		$('#jobid_box').modal('show');
		zw_box.zwShow();
	})
});

//上传证件
function up_zhengjian(){
	    var $ = jQuery,
        $list = $('#fileList'),
        ratio = window.devicePixelRatio || 1,						// 优化retina, 在retina下这个值是2
        thumbnailWidth = 100 * ratio,thumbnailHeight = 100 * ratio,	// 缩略图大小
		uploader; 													// Web Uploader实例		
        allimg = {},											// 所有文件的进度信息，key为file id
	    // 初始化Web Uploader
	    uploader = WebUploader.create({
	        // 自动上传。
	        //auto: true,
	        // swf文件路径
	        swf: '/js/Uploader.swf',
	        // 文件接收服务端。
	        server: '',
	        // 选择文件的按钮。可选。
	        // 内部根据当前运行是创建，可能是input元素，也可能是flash.
	        pick: '#filePicker',
	        // 只允许选择文件，可选。
	        accept: {
	            title: 'Images',
	            extensions: 'gif,jpg,jpeg,bmp,png',
	            mimeTypes: 'image/*'
	        },
	        fileNumLimit:2
	    });
    	// 当有文件添加进来的时候
    	uploader.on( 'fileQueued', function( file ) {
        var $li = $('<li id="' + file.id + '" class="file-item thumbnail"><img><div class="info">' + file.name + '</div></li>'),
        $btns = $('<div class="file-panel"><span class="cancel" title="删除">删除</span><span class="upload" title="重新上传">重新上传</span>').appendTo( $li ),   
        $img = $li.find('img');
		allimg[ file.id ] = [ file.type];
        $list.append( $li );
		$li.on( 'mouseenter', function() {
            $btns.stop().animate({height: 30});
        });
        $li.on( 'mouseleave', function() {
            $btns.stop().animate({height: 0});
        });
        $btns.on( 'click', 'span', function() {
            var index = $(this).index(),deg;
            switch ( index ) {
                case 0:
                    removeFile( file );
                    return;
                case 1:
                    retry( file );
                    return;
            }
            if ( supportTransition ) {
                deg = 'rotate(' + file.rotation + 'deg)';
                $wrap.css({'-webkit-transform': deg,'-mos-transform': deg,'-o-transform': deg,'transform': deg});
            } else {
                $wrap.css( 'filter', 'progid:DXImageTransform.Microsoft.BasicImage(rotation='+ (~~((file.rotation/90)%4 + 4)%4) +')');
            }
        });
        // 创建缩略图
        uploader.makeThumb( file, function( error, src ) {
	            if ( error ) {
	                $img.replaceWith('<span>不能预览</span>');
	                return;
	            }
	            $img.attr( 'src', src );
	        }, thumbnailWidth, thumbnailHeight );
	    });
		// 负责view的销毁
	    function removeFile( file ) {
	        var $li = $('#'+file.id);
	      	delete allimg[ file.id ];
	      	uploader.removeFile( file );
	        $li.off().find('.file-panel').off().end().remove();
	    }
	    function retry(file) {
	    	uploader.retry(file);
	    }
	    // 文件上传过程中创建进度条实时显示。
	    uploader.on( 'uploadProgress', function( file, percentage ) {
	        var $li = $( '#'+file.id ),$percent = $li.find('.progress span');
	        // 避免重复创建
	        if ( !$percent.length ) {
	            $percent = $('<p class="progress"><span></span></p>').appendTo( $li ).find('span');
	        }
	        $percent.css( 'width', percentage * 100 + '%' );
	    });
	    // 文件上传成功，给item添加成功class, 用样式标记上传成功。
	    uploader.on( 'uploadSuccess', function( file ) {
	        $( '#'+file.id ).addClass('upload-state-done');
	    });
	    // 文件上传失败，现实上传出错。
	    uploader.on( 'uploadError', function( file ) {
	        var $li = $( '#'+file.id ),$error = $li.find('div.error');
	        // 避免重复创建
	        if ( !$error.length ) {
	            $error = $('<div class="error"></div>').appendTo( $li );
	        }
	        $error.text('上传失败');
	    });

	    // 完成上传完了，成功或者失败，先删除进度条。
	    uploader.on( 'uploadComplete', function( file ) {
	        $( '#'+file.id ).find('.progress').remove();
	    });
}
//兼职类别选择
var zw_array=[{"Id":1,"PositionName":"促销/导购","IsEnabled":true,"Sort":1,"ReMark":null},{"Id":2,"PositionName":"传单派发","IsEnabled":true,"Sort":2,"ReMark":null},{"Id":3,"PositionName":"钟点工","IsEnabled":true,"Sort":3,"ReMark":null},{"Id":4,"PositionName":"服务员","IsEnabled":true,"Sort":4,"ReMark":null},{"Id":5,"PositionName":"生活配送员","IsEnabled":true,"Sort":5,"ReMark":null},{"Id":6,"PositionName":"护工","IsEnabled":true,"Sort":6,"ReMark":null},{"Id":7,"PositionName":"家教","IsEnabled":true,"Sort":7,"ReMark":null},{"Id":8,"PositionName":"软件开发/编程","IsEnabled":true,"Sort":8,"ReMark":null},{"Id":9,"PositionName":"美工/平面设计","IsEnabled":true,"Sort":9,"ReMark":null}];
var zw_box={
	//加载所有职位
	zwShow : function(){
		var checkH="";//所有职位html
		for(var i in zw_array){
			//console.log(zw_array);
			checkH+='<li><a href="javascript:;" onclick="zw_box.selectjobid('+zw_array[i].Id+''+",'"+ zw_array[i].PositionName +"'"+')">'+ zw_array[i].PositionName +'</a></li>';
		}
		$("#lbbox ul").html(checkH);
	},
	selectjobid:function(id,val){
		$("#jobid").val(id);
		$(".selectlb").html(''+ val +'<span class="caret"></span>');
		$('#jobid_box').modal('hide');
	}
}
