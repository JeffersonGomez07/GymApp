$(document).ready(function () {
    loadMemberships();
});

function loadMemberships() {
    $.ajax({
        url:`${API_URL_BASE}/api/Memberships/GetAllMemberships`,
        method: 'GET',
        success: function (data) {
            renderMembershipList(data);
        },
        error: function (error) {
            console.error("Error al cargar las membresías: ", error);
            Swal.fire({
                title: "Error",
                text: "There was a problem uploading memberships.",
                icon: "error"
            });
        }
    });
}

function renderMembershipList(memberships) {
    let tableBody = $("#membershipTable tbody");
    tableBody.empty();
    memberships.forEach(function (membership) {
        // Formatear el monto con el signo de dólar
        let formattedAmount = parseFloat(membership.amount).toLocaleString('en-US', {
            style: 'currency',
            currency: 'USD'
        });

        // Formatear la fecha de pago
        let paymentDate = new Date(membership.payment_date);
        let formattedPaymentDate = paymentDate.toLocaleDateString('en-US', {
            month: '2-digit',
            day: '2-digit',
            year: 'numeric'
        });

        let row = `<tr>
                <td>${membership.first_name}</td>
                <td>${membership.last_name}</td>
                <td>${membership.membership_type}</td>
                <td>${formattedPaymentDate}</td>
                <td>${formattedAmount}</td>
                <td>${membership.status}</td>
                <td>
                    ${membership.receipt_image ? `<img src="/css/Img/Receipt.png" alt="Receipt" height="50" />` : '<input type="file" class="upload-receipt" />'}
                </td>
                <td>
                    ${membership.receipt_image ? `<button class="approve-btn btn btn-success" 
                            data-user-id="${membership.user_id}" 
                            data-payment-id="${membership.payment_id}" 
                            data-membership-id="${membership.membership_id}">Aprobar</button>` :
                            `<button class="upload-receipt-btn btn btn-primary" 
                        data-payment-id="${membership.payment_id}">Upload receipt</button>`}
                </td>
            </tr>`;
        tableBody.append(row);
    });

    $(".upload-receipt-btn").on("click", function () {
        let row = $(this).closest("tr");
        let fileInput = row.find(".upload-receipt")[0];
        let file = fileInput.files[0];

        if (file) {
            let reader = new FileReader();
            reader.onload = function (e) {
                let receiptImageBase64 = e.target.result.split(',')[1];

                let paymentId = row.find(".upload-receipt-btn").data("payment-id");

                uploadReceiptImage(paymentId, receiptImageBase64);
            };
            reader.readAsDataURL(file);
        } else {
            Swal.fire({
                title: "Error",
                text: "Please select one image.",
                icon: "error"
            });
        }
    });

    $(".approve-btn").on("click", function () {
        let userId = $(this).data("user-id");
        let paymentId = $(this).data("payment-id");
        let membershipId = $(this).data("membership-id");
        approvePayment(userId, paymentId, membershipId);
    });
}

function uploadReceiptImage(paymentId, receiptImageBase64) {
    const data = {
        Payment_id: paymentId,
        Payment_receipt: receiptImageBase64
    };

    console.log(paymentId)


    $.ajax({
        url: `${API_URL_BASE}/api/Memberships/UploadPaymentReceipt`,
        method: 'POST',
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: JSON.stringify(data),
        success: function (data) {
            if (data.error) {
                Swal.fire({
                    title: "Error",
                    text: data.error,
                    icon: "error"
                });
            } else {
                Swal.fire({
                    title: "Success",
                    text: "The payment receipt has been successfully uploaded!",
                    icon: "success"
                }).then(() => {
                    loadMemberships();
                });
            }
        },
        error: function (error) {
            console.error("Error al subir la imagen del recibo: ", error);
            Swal.fire({
                title: "Error",
                text: "There was a problem uploading the payment receipt.",
                icon: "error"
            });
        }
    });
}

function approvePayment(userId, paymentId, membershipId) {
    const data = {
        User_id: userId,
        Payment_id: paymentId,
        Membership_id: membershipId
    };



    $.ajax({
        url: `${API_URL_BASE}/api/Memberships/ApproveMembershipPayment`,
        method: 'POST',
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: JSON.stringify(data),
        success: function (data) {
            if (data.error) {
                Swal.fire({
                    title: "Error",
                    text: data.error,
                    icon: "error"
                    
                });
            } else {
                Swal.fire({
                    title: "Success",
                    text: "The payment has been successfully approved!",
                    icon: "success"
                }).then(() => {
                    loadMemberships();
                });
            }
        },
        error: function (error) {
            console.error("Error approving payment: ", error);
            Swal.fire({
                title: "Error",
                text: "There was a problem approving the payment.",
                icon: "error"
            });
        }
    });
}