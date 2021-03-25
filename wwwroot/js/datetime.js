function realtimeClock() {
    let days = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday']
    let months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December']
    var rtClock = new Date();
    var hours = rtClock.getHours();
    var minutes = rtClock.getMinutes();
    var seconds = rtClock.getSeconds();
    var month = rtClock.getMonth();
    var dayName = days[rtClock.getDay()];
    var todayDate = dayName + "&nbsp;&nbsp;" + months[month] + " " + rtClock.getDate() + ", " + rtClock.getFullYear();

    var amPm = (hours < 12) ? "AM" : "PM";
    hours = (hours > 12) ? hours - 12 : hours;
    minutes = ("0" + minutes).slice(-2);
    seconds = ("0" + seconds).slice(-2);

    document.getElementById('clockMain').innerHTML =
        "&nbsp;&nbsp;" + todayDate + "&nbsp;&nbsp;" + hours + ":" + minutes + ":" + seconds + " " + amPm + "&nbsp;&nbsp;";
    var t = setTimeout(realtimeClock, 500);
}