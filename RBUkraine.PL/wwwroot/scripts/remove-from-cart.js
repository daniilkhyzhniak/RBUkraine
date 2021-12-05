const remove = async (id, onRemove) => {
    return fetch(`https://localhost:5001/cart/remove?id=${id}`,
        {
            method: "post"
        }).then(() => onRemove());
};