async function sendVote(that, postId, isUpVote) {
    const csrfTokenElement = document.getElementsByName("__RequestVerificationToken")[0];

    const json = {
        postId: postId,
        isUpVote: isUpVote
    };

    try {

        const appUrl = "https://192.168.0.198:5001/api/votes";
        const response = await fetch(appUrl, {
            method: "post",
            headers: {
                'RequestVerificationToken': csrfTokenElement.value,
                'Accept': "application/json",
                'Content-Type': "application/json"
            },
            body: JSON.stringify(json)
        });

        const data = await response.json();

        if (response.ok) {

            const upVoteElement = document.getElementById("up-votes");
            const downVoteElement = document.getElementById("down-votes");
            const votesScoreElement = document.getElementById("votes-score");

            upVoteElement.textContent = data.upVotes;
            downVoteElement.textContent = data.downVotes;
            votesScoreElement.textContent = data.votesScore;

            const iElement = that.getElementsByTagName("i")[0];

            if (iElement.classList.contains("fas")) {
                iElement.classList.remove("fas");
                iElement.classList.add("far");
            } else {
                const voteElements = document
                    .querySelectorAll("#votes-display i.fas");

                for (let element of voteElements) {
                    element.classList.remove("fas");
                    element.classList.add("far");
                }

                iElement.classList.remove("far");
                iElement.classList.add("fas");
            }
        }

    } catch (err) {
        console.log(err);
    }
};