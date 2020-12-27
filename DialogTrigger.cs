using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialog;
    private Tasks tasks;

    public void Start()
    {
        tasks = GameObject.Find("Canvas").GetComponent<Tasks>();
    }

    public void TriggerDialog() {
        dialog.sentences = null;

        if (!tasks.Task1 && !tasks.Task2 && !tasks.Task3 && !tasks.Task4 && SceneManager.GetActiveScene().buildIndex == 1)
        {
            dialog.getTask = false;
            dialog.sentences = new string[7];
            dialog.sentences[0] = "Что ты здесь делаешь, дитя? Тебя здесь быть не должно… Тебе надо уйти…";
            dialog.sentences[1] = "Но.. Я не знаю где выход. Можете сказать куда идти?";
            dialog.sentences[2] = "... Прости… Я не могу сказать тебе это просто так. Но, возможно, если ты поможешь мне, я смогу подсказать тебе.";
            dialog.sentences[3] = "Как же так?.. Ладно, в любом случае не думаю, что у меня есть выбор. Что надо сделать?";
            dialog.sentences[4] = "Найди  мой кулон, который я недавно потеряла.";
            dialog.sentences[5] = "Кулон? Хорошо, как он выглядит?";
            dialog.sentences[6] = "Ты задаёшь слишком много вопросов дитя, поверь, он тут такой один.";
            //найди лесника
        }
        if (tasks.Task1 && !tasks.Task2 && !tasks.Task3 && !tasks.Task4 && SceneManager.GetActiveScene().buildIndex == 1)
        {
            dialog.getTask = true;
            dialog.sentences = new string[4];
            dialog.sentences[0] = "Кулон у тебя?";
            dialog.sentences[1] = "Э?.. Нет, пока нет.";
            dialog.sentences[2] = "Тогда тебе стоит поторопиться, скоро совсем стемнеет.";
            dialog.sentences[3] = "Хорошо, я учту, спасибо.";
            //кулон уже найден?
        }
        if (tasks.Task1 && !tasks.Task2 && !tasks.Task3 && !tasks.Task4 && SceneManager.GetActiveScene().buildIndex == 2)
        {
            dialog.getTask = false;
            dialog.sentences = new string[6];
            dialog.sentences[0] = "Агрх, этот лес вам не проходной двор? Что ты здесь делаешь? Проваливай отсюда";
            dialog.sentences[1] = "Я как раз занимаюсь этим… Но сначала мне нужно найти кулон девушки, что я встретила ранее.";
            dialog.sentences[2] = "Какой девушки?";
            dialog.sentences[3] = "Я… Я не знаю, я не спросила её имени. У неё короткие белые волосы. Она не очень высокая и...";
            dialog.sentences[4] = "Всё хватит, ты очень шумная. Раздражаешь. Да, я нашёл вчера кулон, он у меня, но прежде… Я воспользуюсь тем, что ты здесь и попрошу тебя помочь мне, взамен я отдам тебе кулон. Помоги мне собрать некоторые растения.";
            dialog.sentences[5] = "Договорились.";
            //собери растения
        }
        if (tasks.Task1 && tasks.Task2 && !tasks.Task3 && !tasks.Task4 && SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (tasks.Inventary)
            {
                dialog.getTask = false;
                dialog.sentences = new string[4];
                dialog.sentences[0] = "Наконец. Тебе стоит работать над своей скоростью в будущем!";
                dialog.sentences[1] = "Я подумаю над этим...";
                dialog.sentences[2] = "...Ладно, держи кулон. Верни его хозяйке.";
                dialog.sentences[3] = "Спасибо!";
                //вот кулон, верни его
            }
            else {
                dialog.getTask = true;
                dialog.sentences = new string[4];
                dialog.sentences[0] = "О, ты быстро, уже всё собрано?";
                dialog.sentences[1] = "Нет, я хочу спрос...";
                dialog.sentences[2] = "Тогда пошевеливайся!";
                dialog.sentences[3] = "Простите...";
                //растения еще не собраны
            }
        }
        if (tasks.Task1 && tasks.Task2 && tasks.Task3 && !tasks.Task4 && SceneManager.GetActiveScene().buildIndex == 2)
        {
            dialog.getTask = true;
            dialog.sentences = new string[2];
            dialog.sentences[0] = "Что-то ещё? Ты от меня сегодня не отстанешь?";
            dialog.sentences[1] = "Нет, всё в порядке. Я лучше пойду...";
            //кулон уже отдан
        }
        if (tasks.Task1 && tasks.Task2 && tasks.Task3 && !tasks.Task4 && SceneManager.GetActiveScene().buildIndex == 1)
        {
            dialog.getTask = false;
            dialog.sentences = new string[5];
            dialog.sentences[0] = "Он у тебя!? Как же я рада, спасибо! Он очень важен для меня!";
            dialog.sentences[1] = "Я рада была помочь вам, но.. Мне надо домой, вы обещали помочь.";
            dialog.sentences[2] = "Да, прости дитя. Выход находится в лесничего домика.";
            dialog.sentences[3] = "Э?.. Ладно, хорошо, спасибо!";
            dialog.sentences[4] = "Беги-беги, успей до наступления ночи!";
            //отдать кулон, можно выйти из леса
        }
        
        FindObjectOfType<DialogManager>().StartDialog(dialog);
    }
}
