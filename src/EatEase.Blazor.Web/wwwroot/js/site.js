// Selection of HTML objects
const burger = document.querySelector('.burger i');
const nav = document.querySelector('.nav');

// Defining a function
function toggleNav() {
    burger.classList.toggle('fa-bars');
    burger.classList.toggle('fa-times');
    nav.classList.toggle('nav-active');
}

// Calling the function after click event occurs
burger.addEventListener('click', function () {
    toggleNav();
});

// script.js
document.addEventListener('DOMContentLoaded', () => {
    const links = document.querySelectorAll('.vertical-nav .dot');

    links.forEach(link => {
        link.addEventListener('click', (e) => {
            e.preventDefault();
            document.querySelectorAll('.dot').forEach(dot => dot.classList.remove('active'));
            link.classList.add('active');
            const targetId = link.getAttribute('href');
            const targetSection = document.querySelector(targetId);

            window.scrollTo({
                top: targetSection.offsetTop, behavior: 'smooth'
            });
        });
    });

    window.addEventListener('scroll', () => {
        const scrollPos = window.scrollY + window.innerHeight / 2;

        links.forEach(link => {
            const targetId = link.getAttribute('href');
            const targetSection = document.querySelector(targetId);

            if (targetSection.offsetTop <= scrollPos && targetSection.offsetTop + targetSection.offsetHeight > scrollPos) {
                document.querySelectorAll('.dot').forEach(dot => dot.classList.remove('active'));
                link.classList.add('active');
            }
        });
    });
});
