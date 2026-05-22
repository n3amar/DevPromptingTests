# Retrospective Prompt

At the end of your session, copy the prompt below and paste it into the same LLM chat you used during the assessment. Do not start a new conversation — the LLM needs the full context of your session to generate an accurate retrospective.

Save the output as `RETROSPECTIVE.md` in the root of your fork and commit it before submitting.

---

## Prompt to paste

```
We have finished the assessment. I'd like you to act as an honest reviewer and write a structured retrospective of how I used you (the LLM) throughout this session.

Do not be encouraging or diplomatic — be specific and accurate. If I gave you poor context, say so. If I accepted your output without verifying it, note that. If I caught a mistake you made, highlight it as a positive.

Write the retrospective as a markdown document with the following sections:

## Session Summary
A brief (3-5 sentence) overview of what was attempted, what was completed, and what was skipped.

## Challenge Breakdown
For each challenge I worked on, write a short entry covering:
- What approach I took to prompting you
- Whether the first response was usable or needed correction
- Whether I verified your output before accepting it (e.g. ran the code, tested the endpoint, read the diff)
- One specific example of a prompt I gave — was it well-scoped with good context, or vague?

## Initiative and Depth
For each challenge, note whether I stopped at the minimum the task asked for, or whether I questioned the solution further — for example by asking whether the output was complete, robust, or production-ready. Be specific: quote or describe moments where I pushed further, and moments where I accepted the first working result without asking whether it was the best result.

## Prompting Patterns
Summarize patterns you noticed across the session:
- Did I give you enough context (relevant files, error messages, expected behaviour)?
- Did I ask follow-up questions or course-correct when something was off?
- Did I tend to accept output at face value, or did I push back?
- Were my prompts specific or generic?
- Did I ever ask you to evaluate or critique something, rather than just produce it?

## Where I Used You Well
2-3 specific examples from this session where I prompted effectively, pushed for a better answer, or caught an issue with your output.

## Where I Could Have Done Better
2-3 specific examples where my prompting was weak, I gave insufficient context, accepted output I should have questioned, or stopped short of a more complete solution.

## LLM Utilization Score
Rate my overall LLM usage on a scale of 1-5 for each of the following, with a one-sentence justification:
- **Context quality** — how well I gave you the information you needed
- **Verification** — how consistently I checked your output before moving on
- **Iteration** — how effectively I followed up when something wasn't right
- **Critical thinking** — how often I questioned or pushed back on your suggestions
- **Initiative** — how often I went beyond the stated requirement to ask whether the solution was complete or could be improved

---

Now, based specifically on the patterns and weaknesses you observed in this session, generate two additional artifacts I can save to my local Claude Code setup:

## My CLAUDE.md Rules
Write 4-6 rules for my personal CLAUDE.md file derived directly from mistakes or patterns you observed in this session. These should not be generic best practices — they should be specific to how I actually worked today. For example, if I consistently skipped testing endpoints after changes, one rule should address that. If I gave vague prompts when I had the file open right in front of me, say so.

Format each rule as a short imperative sentence followed by one line explaining why it matters based on what you saw today.

## My Prompting Skill
Write a Claude Code skill I can save to `.claude/commands/prompting-checklist.md` that I can invoke before submitting any LLM-assisted change. The skill should be a short checklist (5-7 items) personalised to my weak spots from this session — things I actually skipped or did poorly today, not generic advice. Frame each item as a question I ask myself before moving on.

---

Be honest. The reviewer reading this retrospective is evaluating how well I work with AI tools, not just whether the code works. The rules and skill are for my own benefit — the more accurate they are to my actual behaviour today, the more useful they will be.
```
