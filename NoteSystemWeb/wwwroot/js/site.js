// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

const userBtn = document.getElementById('user-btn');
const navbar = document.getElementById('navbar');

userBtn.addEventListener('click', function(){

    if(navbar.classList[1] == "sidebar"){

        navbar.classList.remove('sidebar');
    }else{

        navbar.classList.add('sidebar');
    }
});