USE Twitt
GO

CREATE VIEW CommentsCount AS
SELECT
    c.MessageID,
    count(*) AS CommentCount
FROM Comment c
GROUP BY c.MessageID

GO