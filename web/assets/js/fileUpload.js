﻿/******* Do not edit this file *******
Simple Custom CSS and JS - by Silkypress.com
Saved: Jan 11 2019 | 08:06:51 */
/* Default comment here */

jQuery(document).ready(function ($) {
    // console.log("iam called");
    //setting footer properties 
    $(".second-footer .textwidget:first").html("Copyright Â© " + (new Date()).getFullYear() + "Â  eOpen.com").attr('style', 'color:#fff');

    // carer model handle here 
    // $( "#btnappmodel" ).click(function() {
    // console.log("i am clicked");
    // $('#applicationmodel').modal('show'); 

    /*cv uploading*/

    var uploadedFiles = [];
    function initJqueryUpload(data) {
        console.log("Init Jquery Upload");
        var self = this;
        $('#image-cover').fileupload({
            url: "/ContactUs.aspx/UploadCv",//"/assets/ContactUs.aspx/UploadCv",
            autoUpload: false,
            dataType: 'json',
            maxFileSize: 500,
            replaceFileInput: false,
            send: function (e, data) {
                console.log('Sending data...');
                data.formData.timeStamp = (+new Date());
                console.log(data.formData.timeStamp);
            },
            add: function (e, data) {
                console.log(data.originalFiles[0]);
                var acceptFileTypes = /^application\/(pdf|vnd.openxmlformats-officedocument.wordprocessingml.document|msword)$/i;
                if (data.originalFiles[0]['type'].length && !acceptFileTypes.test(data.originalFiles[0]['type'])) {
                    $('#file-name').addClass("cv-error");
                    $("#file-name").text('Please select pdf and docx file.');
                    //$("#btnUpload").prop("disabled", true);
                } else if (data.originalFiles[0]['size'] > 10489900) {
                    alert('File Size');
                } else {
                    uploadedFiles = [];
                    $.each(data.files, function (index, file) {
                        uploadedFiles.push(file);
                        console.log('Added file: ' + file.name);
                        $('#file-name').removeClass("cv-error");
                        $("#btnUpload").prop("disabled", false);
                        $("#file-name").text(file.name);

                        console.log("adding files ==>");
                    });
                    console.log(uploadedFiles);
                }
            }
            //progressall: function (e, data) {
            //    var progress = parseInt(data.loaded / data.total * 100, 10);
            //    $('#progress .bar').css(
            //        'width',
            //        progress + '%'
            //    );
            //    alert("progress");
            //}
        });
    }

    function uploadImage(callback) {
        console.log("In Call Back");
        console.log(uploadedFiles);
        $('#image-cover').fileupload('send', { files: uploadedFiles })
            .done(function (result, textStatus, jqXHR) {

                $("#shareModal").modal("hide");
                $("#successModal").modal("show");
                console.log(result)
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $('#file-name').addClass("cv-error");
                $("#file-name").text('Please select pdf and docx file.');
            })
    }
    //initJqueryUpload();
    $("#btnUpload").click(function () {
        uploadImage();
        $("#btnUpload").prop("disabled", true);
    });
    $("#applyNow").click(function () {
        $("#shareModal").modal("show");
        $("#btnUpload").prop("disabled", false);
    });


});