using Civiti.Api.Models.Email;
using static Civiti.Api.Infrastructure.Email.EmailDataKeys;

namespace Civiti.Api.Infrastructure.Email;

/// <summary>
/// Static Romanian email templates for all 14 notification types.
/// Each method returns (subject, bodyHtml) for a given template data dictionary.
/// </summary>
public static class EmailTemplates
{
    public static (string Subject, string BodyHtml) Get(EmailNotificationType type, Dictionary<string, string> data)
    {
        return type switch
        {
            EmailNotificationType.IssueSubmitted => IssueSubmitted(data),
            EmailNotificationType.IssueApproved => IssueApproved(data),
            EmailNotificationType.IssueRejected => IssueRejected(data),
            EmailNotificationType.ChangesRequested => ChangesRequested(data),
            EmailNotificationType.IssueResolved => IssueResolved(data),
            EmailNotificationType.IssueCancelled => IssueCancelled(data),
            EmailNotificationType.NewCommentOnIssue => NewCommentOnIssue(data),
            EmailNotificationType.ReplyToComment => ReplyToComment(data),
            EmailNotificationType.VoteMilestone => VoteMilestone(data),
            EmailNotificationType.EmailSupportMilestone => EmailSupportMilestone(data),
            EmailNotificationType.LevelUp => LevelUp(data),
            EmailNotificationType.BadgeEarned => BadgeEarned(data),
            EmailNotificationType.AchievementCompleted => AchievementCompleted(data),
            EmailNotificationType.Welcome => Welcome(data),
            _ => ("Notificare Civiti", "<p>Ai o notificare noua pe Civiti.</p>")
        };
    }

    private static (string, string) IssueSubmitted(Dictionary<string, string> d)
    {
        var title = d.GetValueOrDefault(IssueTitle, "");
        var userName = d.GetValueOrDefault(UserName, "");
        return (
            "Problema ta a fost trimisa",
            "<p>Salut, <strong>" + userName + "</strong>!</p>" +
            "<p>Problema ta <strong>" + title + "</strong> a fost trimisa cu succes si este acum in asteptarea revizuirii de catre echipa noastra de moderare.</p>" +
            "<p>Vei primi o notificare cand problema ta va fi revizuita.</p>"
        );
    }

    private static (string, string) IssueApproved(Dictionary<string, string> d)
    {
        var title = d.GetValueOrDefault(IssueTitle, "");
        var userName = d.GetValueOrDefault(UserName, "");
        return (
            "Problema ta a fost aprobata!",
            "<p>Vesti bune, <strong>" + userName + "</strong>!</p>" +
            "<p>Problema ta <strong>" + title + "</strong> a fost aprobata si este acum vizibila comunitatii.</p>" +
            "<p>Ai primit <strong style=\"color: #FCA311;\">+50 puncte</strong> pentru contributia ta!</p>" +
            "<p>Comunitatea poate acum sa voteze si sa trimita emailuri de sustinere catre autoritati.</p>"
        );
    }

    private static (string, string) IssueRejected(Dictionary<string, string> d)
    {
        var title = d.GetValueOrDefault(IssueTitle, "");
        var userName = d.GetValueOrDefault(UserName, "");
        var reason = d.GetValueOrDefault(Reason, "Nu a fost specificat un motiv.");
        return (
            "Problema ta nu a fost aprobata",
            "<p>Salut, <strong>" + userName + "</strong>.</p>" +
            "<p>Din pacate, problema ta <strong>" + title + "</strong> nu a fost aprobata de echipa de moderare.</p>" +
            "<p><strong>Motivul:</strong> " + reason + "</p>" +
            "<p>Poti crea o noua problema tinand cont de feedback-ul primit.</p>"
        );
    }

    private static (string, string) ChangesRequested(Dictionary<string, string> d)
    {
        var title = d.GetValueOrDefault(IssueTitle, "");
        var userName = d.GetValueOrDefault(UserName, "");
        var notes = d.GetValueOrDefault(Notes, "");
        return (
            "Modificari solicitate pentru problema ta",
            "<p>Salut, <strong>" + userName + "</strong>.</p>" +
            "<p>Echipa de moderare a solicitat modificari la problema ta <strong>" + title + "</strong>.</p>" +
            "<p><strong>Note de la moderator:</strong></p>" +
            "<div style=\"background-color: #F5F5F5; padding: 12px 16px; border-left: 3px solid #FCA311; border-radius: 4px; margin: 12px 0;\">" +
            notes +
            "</div>" +
            "<p>Te rugam sa editezi problema si sa o retrimiti.</p>"
        );
    }

    private static (string, string) IssueResolved(Dictionary<string, string> d)
    {
        var title = d.GetValueOrDefault(IssueTitle, "");
        var userName = d.GetValueOrDefault(UserName, "");
        return (
            "Problema a fost rezolvata!",
            "<p>Salut, <strong>" + userName + "</strong>!</p>" +
            "<p>Problema <strong>" + title + "</strong> a fost marcata ca rezolvata.</p>" +
            "<p>Multumim tuturor celor care au contribuit la rezolvarea acestei probleme!</p>"
        );
    }

    private static (string, string) IssueCancelled(Dictionary<string, string> d)
    {
        var title = d.GetValueOrDefault(IssueTitle, "");
        var userName = d.GetValueOrDefault(UserName, "");
        return (
            "Problema a fost anulata",
            "<p>Salut, <strong>" + userName + "</strong>.</p>" +
            "<p>Problema <strong>" + title + "</strong> la care ai participat a fost anulata de autorul ei.</p>"
        );
    }

    private static (string, string) NewCommentOnIssue(Dictionary<string, string> d)
    {
        var title = d.GetValueOrDefault(IssueTitle, "");
        var userName = d.GetValueOrDefault(UserName, "");
        var commenterName = d.GetValueOrDefault(CommenterName, "Cineva");
        var excerpt = d.GetValueOrDefault(CommentExcerpt, "");
        return (
            "Comentariu nou la problema ta",
            "<p>Salut, <strong>" + userName + "</strong>!</p>" +
            "<p><strong>" + commenterName + "</strong> a lasat un comentariu la problema ta <strong>" + title + "</strong>:</p>" +
            "<div style=\"background-color: #F5F5F5; padding: 12px 16px; border-left: 3px solid #14213D; border-radius: 4px; margin: 12px 0; font-style: italic;\">" +
            excerpt +
            "</div>"
        );
    }

    private static (string, string) ReplyToComment(Dictionary<string, string> d)
    {
        var userName = d.GetValueOrDefault(UserName, "");
        var replierName = d.GetValueOrDefault(ReplierName, "Cineva");
        var excerpt = d.GetValueOrDefault(ReplyExcerpt, "");
        return (
            "Raspuns la comentariul tau",
            "<p>Salut, <strong>" + userName + "</strong>!</p>" +
            "<p><strong>" + replierName + "</strong> a raspuns la comentariul tau:</p>" +
            "<div style=\"background-color: #F5F5F5; padding: 12px 16px; border-left: 3px solid #14213D; border-radius: 4px; margin: 12px 0; font-style: italic;\">" +
            excerpt +
            "</div>"
        );
    }

    private static (string, string) VoteMilestone(Dictionary<string, string> d)
    {
        var title = d.GetValueOrDefault(IssueTitle, "");
        var userName = d.GetValueOrDefault(UserName, "");
        var count = d.GetValueOrDefault(VoteCount, "0");
        return (
            "Problema ta a atins " + count + " voturi!",
            "<p>Felicitari, <strong>" + userName + "</strong>!</p>" +
            "<p>Problema ta <strong>" + title + "</strong> a atins un nou prag: <strong style=\"color: #FCA311;\">" + count + " voturi</strong> din partea comunitatii!</p>" +
            "<p>Continua sa faci diferenta in comunitatea ta.</p>"
        );
    }

    private static (string, string) EmailSupportMilestone(Dictionary<string, string> d)
    {
        var title = d.GetValueOrDefault(IssueTitle, "");
        var userName = d.GetValueOrDefault(UserName, "");
        var count = d.GetValueOrDefault(EmailCount, "0");
        return (
            "Problema ta: " + count + " emailuri trimise!",
            "<p>Felicitari, <strong>" + userName + "</strong>!</p>" +
            "<p>Comunitatea a trimis <strong style=\"color: #FCA311;\">" + count + " emailuri</strong> catre autoritati in sprijinul problemei tale <strong>" + title + "</strong>!</p>" +
            "<p>Presiunea civica functioneaza!</p>"
        );
    }

    private static (string, string) LevelUp(Dictionary<string, string> d)
    {
        var userName = d.GetValueOrDefault(UserName, "");
        var level = d.GetValueOrDefault(Level, "");
        return (
            "Ai avansat la nivelul " + level + "!",
            "<p>Felicitari, <strong>" + userName + "</strong>!</p>" +
            "<p>Ai atins <strong style=\"color: #FCA311;\">Nivelul " + level + "</strong> pe Civiti!</p>" +
            "<p>Continua activitatea civica pentru a avansa si mai mult.</p>"
        );
    }

    private static (string, string) BadgeEarned(Dictionary<string, string> d)
    {
        var userName = d.GetValueOrDefault(UserName, "");
        var badgeName = d.GetValueOrDefault(BadgeName, "");
        return (
            "Ai castigat insigna " + badgeName + "!",
            "<p>Felicitari, <strong>" + userName + "</strong>!</p>" +
            "<p>Ai castigat o insigna noua: <strong style=\"color: #FCA311;\">" + badgeName + "</strong></p>" +
            "<p>Viziteaza profilul tau pentru a vedea toate insignele castigate.</p>"
        );
    }

    private static (string, string) AchievementCompleted(Dictionary<string, string> d)
    {
        var userName = d.GetValueOrDefault(UserName, "");
        var achievementName = d.GetValueOrDefault(AchievementName, "");
        return (
            "Realizare completata: " + achievementName + "!",
            "<p>Felicitari, <strong>" + userName + "</strong>!</p>" +
            "<p>Ai completat realizarea: <strong style=\"color: #FCA311;\">" + achievementName + "</strong></p>" +
            "<p>Descopera ce alte realizari te asteapta pe Civiti.</p>"
        );
    }

    private static (string, string) Welcome(Dictionary<string, string> d)
    {
        var userName = d.GetValueOrDefault(UserName, "");
        return (
            "Bine ai venit pe Civiti!",
            "<p>Salut, <strong>" + userName + "</strong>!</p>" +
            "<p>Iti multumim ca te-ai alaturat comunitatii <strong>Civiti</strong> — platforma civica a cetatenilor.</p>" +
            "<p>Iata ce poti face pe Civiti:</p>" +
            "<ul style=\"padding-left: 20px; margin: 12px 0;\">" +
            "<li style=\"margin-bottom: 8px;\"><strong>Raporteaza probleme</strong> din comunitatea ta</li>" +
            "<li style=\"margin-bottom: 8px;\"><strong>Voteaza</strong> problemele care te afecteaza</li>" +
            "<li style=\"margin-bottom: 8px;\"><strong>Trimite emailuri</strong> catre autoritati pentru a exercita presiune civica</li>" +
            "<li style=\"margin-bottom: 8px;\"><strong>Castiga puncte si insigne</strong> pentru activitatea ta civica</li>" +
            "</ul>" +
            "<p>Impreuna putem face diferenta!</p>"
        );
    }
}
