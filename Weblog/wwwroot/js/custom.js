$(document).ready(function () {
    console.log("Ready");
    LoadCKEditor4();
});

function LoadCKEditor4() { // نام تابع باید صحیح باشد
    console.log("LoadCKEditor4");

    // بررسی وجود عنصر با ID مشخص
    if (!document.getElementById("ckEditor4")) {
        console.log("Element 'ckEditor4' not found.");
        return;
    }

    console.log("Element 'ckEditor4' found. Loading CKEditor...");

    // اضافه کردن اسکریپت CKEditor
    if (!$("script[src='/ckeditor4/ckeditor/ckeditor.js']").length) {
        $("body").append("<script src='/ckeditor4/ckeditor/ckeditor.js'></script>");
    }

    // جایگزینی عنصر با CKEditor
    CKEDITOR.replace('ckEditor4', {
        customConfig: '/ckeditor4/ckeditor/config.js' // مسیر درست فایل کانفیگ
    });
}
