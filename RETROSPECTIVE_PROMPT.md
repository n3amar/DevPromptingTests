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

## Prompting Patterns
Summarize patterns you noticed across the session:
- Did I give you enough context (relevant files, error messages, expected behaviour)?
- Did I ask follow-up questions or course-correct when something was off?
- Did I tend to accept output at face value, or did I push back?
- Were my prompts specific ("fix the LINQ query in GetByProject that returns the wrong tasks") or generic ("fix the bug")?

## Where I Used You Well
2-3 specific examples from this session where I prompted effectively or caught an issue with your output.

## Where I Could Have Done Better
2-3 specific examples where my prompting was weak, I gave insufficient context, or I accepted output I should have questioned.

## LLM Utilization Score
Rate my overall LLM usage on a scale of 1-5 for each of the following, with a one-sentence justification:
- **Context quality** — how well I gave you the information you needed
- **Verification** — how consistently I checked your output before moving on
- **Iteration** — how effectively I followed up when something wasn't right
- **Critical thinking** — how often I questioned or pushed back on your suggestions

Be honest. The reviewer reading this retrospective is evaluating how well I work with AI tools, not just whether the code works.
```
