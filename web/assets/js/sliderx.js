function changeSlider(id) {
    //console.log("called");
    var sliderx_items = document.getElementsByClassName("sliderx_item");
    for (var i = 0; i < sliderx_items.length; i++) {
        var item = sliderx_items[i];
        item.classList.remove("active");
    }

    var sliderx_txt_items = document.getElementsByClassName("sliderx_txt");
    for (var i = 0; i < sliderx_txt_items.length; i++) {
        var item = sliderx_txt_items[i];
        item.classList.remove("active");
    }

    var dots = document.getElementsByClassName("slide_navi_dots")[0].getElementsByTagName("div");
    for (var i = 0; i < dots.length; i++) {
        var dot = dots[i];
        dot.classList.remove("active");
    }
    if (id == "one") {
        document.getElementById("sliderx_one").classList.add("active");
        document.getElementById("sliderx_txt_one").classList.add("active");
        document.getElementById("dot_one").classList.add("active");
    }
    if (id == "two") {
        document.getElementById("sliderx_two").classList.add("active");
        document.getElementById("sliderx_txt_two").classList.add("active");
        document.getElementById("dot_two").classList.add("active");
    }
    if (id == "three") {
        document.getElementById("sliderx_three").classList.add("active");
        document.getElementById("sliderx_txt_three").classList.add("active");
        document.getElementById("dot_three").classList.add("active");
    }
    if (id == "four") {
        document.getElementById("sliderx_four").classList.add("active");
        document.getElementById("sliderx_txt_four").classList.add("active");
        document.getElementById("dot_four").classList.add("active");
    }
}

function showSlider() {
    //console.log("showSlider");
    var sliderx = document.getElementById("sliderx");
    if (!sliderx.classList.contains("active")) {
        sliderx.classList.add("active");
    }
}
function hideSlider() {
    //console.log("hideSlider");
    var sliderx = document.getElementById("sliderx");
    if (sliderx.classList.contains("active")) {
        sliderx.classList.remove("active");
    }
}