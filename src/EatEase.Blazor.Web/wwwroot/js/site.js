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

function registerSectionObserver() {
    document.querySelectorAll("section")
        .forEach((section) => {
            new IntersectionObserver((entries, observer) => {
                entries.forEach((entry) => {
                    if (entry.isIntersecting) {
                        console.log(entry.target);
                        let current = document.querySelector(`.scroll-indicator a[href='#${entry.target.id}']`);
                        document.querySelectorAll(".scroll-indicator a").forEach(dot => {
                            dot.classList.remove("active")
                        });
                        current.classList.add("active");
                    }
                })
            }, {threshold: 0.5}).observe(section);
        });
}