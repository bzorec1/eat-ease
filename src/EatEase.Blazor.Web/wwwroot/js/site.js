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

window.jsInterop = {
    scrollIndicator: function () {
        const dots = document.querySelectorAll(".scroll-indicator a");

        const removeActiveClass = () => {
            dots.forEach(dot => {
                dot.classList.remove("active")
            });
        };

        const addActiveClass = (entries, observer) => {
            entries.forEach((entry) => {
                if (entry.isIntersecting) {
                    console.log(entry.target);
                    let current = document.querySelector(
                        `.scroll-indicator a[href='#${entry.target.id}']`
                    );
                    removeActiveClass();
                    current.classList.add("active");
                }
            })
        };

        const options = {
            threshold: 0.5,
        };

        const observer = new IntersectionObserver(addActiveClass, options);
        const sections = document.querySelectorAll("section");

        sections.forEach((section) => {
            observer.observe(section);
        });
    }
}
