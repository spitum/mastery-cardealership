let x = document.querySelectorAll(".money");
for (let i = 0, len = x.length; i < len; i++) {
    let num = Number(x[i].innerHTML)
        .toLocaleString('en');
    x[i].innerHTML = num;
    x[i].classList.add("currSign");
} 