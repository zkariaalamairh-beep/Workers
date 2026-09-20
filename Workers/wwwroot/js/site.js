document.addEventListener("DOMContentLoaded", function () {
    const nearbyButtons =
        document.querySelectorAll(".js-nearby-providers");

    nearbyButtons.forEach(function (button) {
        button.addEventListener("click", function () {
            if (!navigator.geolocation) {
                alert("متصفحك لا يدعم تحديد الموقع.");
                return;
            }

            const originalContent = button.innerHTML;

            button.disabled = true;
            button.textContent = "جاري تحديد موقعك...";

            navigator.geolocation.getCurrentPosition(
                function (position) {
                    const latitude = position.coords.latitude;
                    const longitude = position.coords.longitude;

                    const targetUrl =
                        button.dataset.url || "/Providers/Nearby";

                    const url = new URL(
                        targetUrl,
                        window.location.origin
                    );

                    url.searchParams.set(
                        "latitude",
                        latitude.toString()
                    );

                    url.searchParams.set(
                        "longitude",
                        longitude.toString()
                    );

                    window.location.href = url.toString();
                },
                function (error) {
                    let message = "تعذر تحديد موقعك.";

                    if (error.code === 1) {
                        message =
                            "لم يتم السماح بالوصول إلى الموقع.";
                    } else if (error.code === 2) {
                        message =
                            "تعذر العثور على موقعك الحالي.";
                    } else if (error.code === 3) {
                        message =
                            "انتهت مهلة تحديد الموقع.";
                    }

                    alert(message);

                    button.disabled = false;
                    button.innerHTML = originalContent;
                },
                {
                    enableHighAccuracy: true,
                    timeout: 10000,
                    maximumAge: 300000
                }
            );
        });
    });
});