const confirmApplication = (id) => {
    return fetch(`https://localhost:5001/volunteers/${id}/confirm`,
        {
            method: "post"
        }).then(() => location.reload());
};

const declineApplication = (id) => {
    return fetch(`https://localhost:5001/volunteers/${id}/decline`,
        {
            method: "post"
        }).then(() => location.reload());
};

