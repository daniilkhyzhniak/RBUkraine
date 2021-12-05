const addToCart = async (id) => {
    return fetch(`https://localhost:5001/cart/${id}/add`,
        {
            method: "post"
        });
};