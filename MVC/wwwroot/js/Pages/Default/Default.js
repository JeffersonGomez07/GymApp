document.addEventListener("DOMContentLoaded", function () {
    const membershipButtons = document.querySelectorAll(".membership-button");
    const userId = localStorage.getItem('userId');

    const handlePayment = (event) => {
        const button = event.currentTarget;
        const membershipType = button.dataset.membershipType;
        const amount = button.dataset.amount;

        Swal.fire({
            title: `Purchase ${membershipType} Membership`,
            html: `
                <form id="paymentForm" enctype="multipart/form-data">
                    <label for="paymentMethod">Payment Method:</label>
                    <select id="paymentMethod" name="paymentMethod" class="swal2-input">
                        <option value="Cash">Cash</option>
                        <option value="Bank Transfer">Bank Transfer</option>
                        <option value="Sinpe">Sinpe Moóvil</option>
                    </select>
                    <label for="receiptImage">Upload Receipt:</label>
                    <input type="file" id="receiptImage" name="receiptImage" class="swal2-input" accept="image/*" required />
                    <input type="hidden" name="membershipType" value="${membershipType}" />
                    <input type="hidden" name="amount" value="${amount}" />
                </form>
            `,
            showCancelButton: true,
            confirmButtonText: 'Submit Payment',
            preConfirm: () => {
                const form = Swal.getPopup().querySelector('#paymentForm');
                const formData = new FormData(form);
                return formData;
            }
        }).then((result) => {
            if (result.isConfirmed) {
                const formData = result.value;
                const receiptImageFile = formData.get('receiptImage');

                if (receiptImageFile) {
                    const reader = new FileReader();

                    reader.onload = function () {
                        formData.append('receiptImageBase64', reader.result.split(',')[1]);
                        formData.append('userId', userId);

                        $.ajax({
                            url: `${API_URL_BASE}/api/Memberships/ProcessPayment`,
                            type: 'POST',
                            data: formData,
                            processData: false,
                            contentType: false,
                            success: function (data) {
                                if (data.error) {
                                    Swal.fire({
                                        title: 'Error!',
                                        text: data.error,
                                        icon: 'error'
                                    });
                                } else {
                                    Swal.fire({
                                        title: 'Success!',
                                        text: 'Your payment has been sent and is pending approval!',
                                        icon: 'success'
                                    }).then(() => {
                                        // Redirigir a la página de confirmación o de inicio
                                        window.location.href = "/Default/DefaultPage";
                                    });
                                }
                            },
                            error: function (jqXHR, status, error) {
                                let errorMessage = "Unknown error occurred.";
                                if (jqXHR.responseJSON && jqXHR.responseJSON.error) {
                                    errorMessage = jqXHR.responseJSON.error;
                                } else if (jqXHR.responseText) {
                                    errorMessage = jqXHR.responseText;
                                } else if (error) {
                                    errorMessage = error;
                                }
                                Swal.fire({
                                    title: 'Error!',
                                    text: errorMessage,
                                    icon: 'error'
                                });
                            }
                        });
                    };

                    reader.readAsDataURL(receiptImageFile);
                } else {
                    Swal.fire({
                        title: 'Error!',
                        text: 'Please upload a receipt image.',
                        icon: 'error'
                    });
                }
            }
        });
    };

    membershipButtons.forEach(button => {
        button.addEventListener("click", handlePayment);
    });
});