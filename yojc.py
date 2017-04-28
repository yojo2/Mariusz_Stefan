#!/usr/bin/env python
# -*- coding: utf-8 -*- 

import random
import os
import sqlite3
import sys
import types
import re
import itertools
import time

boruc = 'Artur Boruc'

def act(msg):
    """docstring for act"""
    action = '\x01ACTION %s\x01' % msg
    return action

def replace_all(text, dic):
    for i, j in dic.iteritems():
        text = text.replace(i, j)
    return text

def f_set(phenny, input):
    global boruc
    
    if input.group(2) != None:
        boruc = input.group(2).rstrip()
    else:
        boruc = 'Artur Boruc';
    
f_set.commands = ['set']

# -------------------------------------
# kto / kim / komu / kogo
# -------------------------------------

def f_gdzie(phenny, input):
    if input != ".gdzie":
        seed = ''.join(ch for ch, _ in itertools.groupby(''.join(sorted(re.sub("[^a-z0-9]", "", replace_all(input, {u'Ą':'A', u'Ę':'E', u'Ó':'O', u'Ś':'S', u'Ł':'L', u'Ż':'Z', u'Ź':'Z', u'Ć':'C', u'Ń':'N', u'ą':'a', u'ę':'e', u'ó':'o', u'ś':'s', u'ł':'l', u'ż':'z', u'ź':'z', u'ć':'c', u'ń':'n'}).lower())[3:]))))
        random.seed(seed)
    phenny.reply(random.choice(["pod mostem", "w dupie " + random.choice(["Turambara", "yojca", "Lorda Nargogha", "Rankina", "Kathai", "Behemorta", "orgieła", "Stillborna", "Metalusa", "kicka", "podbiela", "Sedinusa", "Hakkena", "Tebega", "Sermacieja", "t3trisa", "optiego", "Hrabuli", "FaceDancera", "Holiego.Deatha", "lghosta", "POLIPa", "mateusza(stefana)", "Xysia", "Germanotty", "Berlina", "8azyliszka", "Seekera", "Murezora", "RIPa", "Aidena", "Trepliev", "Accouna"]), "u optiego", "na wydziale elektrycznym", "w Kathowicach", "u Kath w piwnicy"]))
f_gdzie.commands = ['gdzie']


def f_kim(phenny, input):
    if input != ".kim":
        seed = ''.join(ch for ch, _ in itertools.groupby(''.join(sorted(re.sub("[^a-z0-9]", "", replace_all(input, {u'Ą':'A', u'Ę':'E', u'Ó':'O', u'Ś':'S', u'Ł':'L', u'Ż':'Z', u'Ź':'Z', u'Ć':'C', u'Ń':'N', u'ą':'a', u'ę':'e', u'ó':'o', u'ś':'s', u'ł':'l', u'ż':'z', u'ź':'z', u'ć':'c', u'ń':'n'}).lower())[3:]))))
        random.seed(seed)
    phenny.reply(random.choice(["Turambarem", "yojecem", "Lordem Nargoghiem", "Rankinem", "Kathaicem", "Behemortem", "orgiełem", "Stillbornem", "Metalusem", "kickiem", "podbielem", "Sedinusem", "Hakkenem", "Tebegiem", "Sermaciejem", "t3trisem", "optim", "Hrabulą", "FaceDancerem", "Holim.Death", "lghostem", "POLIPem", "mateuszem(stefanem)", "Xysiem", "Germanotta", "Berlinem", "8azyliszkiem", "Seekerem", "Murezorem", "RIPem", "Aidenem", "Trepliev", "Accounem"]))
f_kim.commands = ['kim']

def f_kto(phenny, input):
    if input != ".kto":
        seed = ''.join(ch for ch, _ in itertools.groupby(''.join(sorted(re.sub("[^a-z0-9]", "", replace_all(input, {u'Ą':'A', u'Ę':'E', u'Ó':'O', u'Ś':'S', u'Ł':'L', u'Ż':'Z', u'Ź':'Z', u'Ć':'C', u'Ń':'N', u'ą':'a', u'ę':'e', u'ó':'o', u'ś':'s', u'ł':'l', u'ż':'z', u'ź':'z', u'ć':'c', u'ń':'n'}).lower())[3:]))))
        random.seed(seed)
    phenny.reply(random.choice(["Turambar", "yojec", "Lord Nargogh", "Rankin", "Kathai", "Behemort", "orgiełe", "Stillborn", "Metalus", "kicek", "podbiel", "Sedinus", "Hakken", "Tebeg", "Sermaciej", "t3tris", "opti", "Hrabula", "FaceDancer", "Holy.Death", "lghost", "POLIP", "mateusz(stefan)", "Xysiu", "Germanotta", "Berlin", "8azyliszek", "Seeker", "Murezor", "RIP", "Aiden", "Trepliev", "Accoun"]))
f_kto.commands = ['kto']

def f_kogo(phenny, input):
    if input != ".kogo":
        seed = ''.join(ch for ch, _ in itertools.groupby(''.join(sorted(re.sub("[^a-z0-9]", "", replace_all(input, {u'Ą':'A', u'Ę':'E', u'Ó':'O', u'Ś':'S', u'Ł':'L', u'Ż':'Z', u'Ź':'Z', u'Ć':'C', u'Ń':'N', u'ą':'a', u'ę':'e', u'ó':'o', u'ś':'s', u'ł':'l', u'ż':'z', u'ź':'z', u'ć':'c', u'ń':'n'}).lower())[3:]))))
        random.seed(seed)
    if input[:5] == ".kogo":
        hrabul = "Hrabulę"
    else:
        hrabul = "Hrabuli"
    
    phenny.reply(random.choice(["Turambara", "yojca", "Lorda Nargogha", "Rankina", "Kathai", "Behemorta", "orgieła", "Stillborna", "Metalusa", "kicka", "podbiela", "Sedinusa", "Hakkena", "Tebega", "Sermacieja", "t3trisa", "optiego", hrabul, "FaceDancera", "Holiego.Deatha", "lghosta", "POLIPa", "mateusza(stefana)", "Xysia", "Germanotty", "Berlina", "8azyliszka", "Seekera", "Murezora", "RIPa", "Aidena", "Trepliev", "Accouna"]))
f_kogo.commands = ['czyim|kogo|czyj(a|e)?']

def f_komu(phenny, input):
    if input != ".komu":
        seed = ''.join(ch for ch, _ in itertools.groupby(''.join(sorted(re.sub("[^a-z0-9]", "", replace_all(input, {u'Ą':'A', u'Ę':'E', u'Ó':'O', u'Ś':'S', u'Ł':'L', u'Ż':'Z', u'Ź':'Z', u'Ć':'C', u'Ń':'N', u'ą':'a', u'ę':'e', u'ó':'o', u'ś':'s', u'ł':'l', u'ż':'z', u'ź':'z', u'ć':'c', u'ń':'n'}).lower())[3:]))))
        random.seed(seed)
    phenny.reply(random.choice(["Turambarowi", "yojcu", "Lordowi Nargoghowi", "Rankinowi", "Kathajce", "Behemortowi", "orgiełowi", "Stillbornowi", "Metalusowi", "kickowi", "podbielowi", "Sedinusowi", "Hakkenowi", "Tebegowi", "Sermaciejowi", "t3trisowi", "optiemu", "Hrabuli", "FaceDancerowi", "Holiemu.Deathowi", "lghostowi", "POLIPowi", "mateuszowi(stefanowi)", "Xysiowi", "Germanotcie", "Berlinowi", "8azyliszkowi", "Seekerowi", "Murezorowi", "R1Powi", "Aidenowi", "Trepliev", "Accounowi"]))
f_komu.commands = ['komu']

# -------------------------------------
# gaywards
# -------------------------------------

def f_ohshitimsorry(phenny, input):
    phenny.say("sorry for what?")
f_ohshitimsorry.commands = ['$oh shit I(\')?m sorry']

def f_sorryforwhat(phenny, input):
    phenny.say("our dad told us not to be ashamed of our dicks")
f_sorryforwhat.commands = ['$sorry for what']

def f_nottobeashamed(phenny, input):
    phenny.say("especially since they're such good size and all")
f_nottobeashamed.commands = ['$our dad told us not to be ashamed of our dicks']

def f_iseethat(phenny, input):
    phenny.say("yeah, I see that")
f_iseethat.commands = ['$specially since (it(\')?s|theyre|they\'re) such good size']

def f_goodadvice(phenny, input):
    phenny.say("daddy gave you good advice")
f_goodadvice.commands = ['$yea(h)?(,)? i see that']

def f_itgetsbigger(phenny, input):
    phenny.say("it gets bigger when I pull on it")
f_itgetsbigger.commands = ['$daddy gave you good advice']

def f_mmmm(phenny, input):
    ret = ""
    for x in range(0, random.randint(4, 13)):
        ret += 'm'
    for x in range(0, random.randint(1, 4)):
        ret += 'M'
    for x in range(0, random.randint(2, 5)):
        ret += 'H'
    for x in range(0, random.randint(1, 4)):
        ret += 'M'
    for x in range(0, random.randint(4, 13)):
        ret += 'm'
    phenny.say(ret)
f_mmmm.commands = ['$it gets bigger when I pull', 'm{3,}']

def f_iriptheskin(phenny, input):
    phenny.say("sometimes I pull it on so hard, I rip the skin!")
f_iriptheskin.commands = ['$[mh]{12,}']

def f_mydaddytold(phenny, input):
    phenny.say("my daddy told me few things too")
f_mydaddytold.commands = ['$sometimes I pull it on so hard(,)? I rip the skin']

def f_nottorip(phenny, input):
    phenny.say("like, uh, how not to rip the skin by using someone else's mouth")
f_nottorip.commands = ['$my daddy (told|taught) me few things too']

def f_willyoushowme(phenny, input):
    phenny.say("will you show me?")
f_willyoushowme.commands = ['$how not to rip the skin by using someone else(\')?s mouth']

def f_idberighthappy(phenny, input):
    phenny.say("I'd be right happy to!")
f_idberighthappy.commands = ['$will you show me']

def f_gaysex(phenny, input):
    phenny.say("[GAY SEX SCENE]")
f_gaysex.commands = ['$I(\')?d be right happy to']


# -------------------------------------
# funkcje sprawdzające całą wypowiedź
# -------------------------------------

def f_cogowno(phenny, input):
    if random.randint(0, 20) == 0:
        phenny.reply("gówno 1:0")
f_cogowno.commands = ['$$co|czo']

def f_czyzby(phenny, input):
    if random.randint(0, 4) == 0:
        phenny.reply("chyba ty")
f_czyzby.commands = [u'$czyzby|czyżby']

def f_dick(phenny, input):
    if random.randint(0, 1) == 0:
        ret = "8"
        for x in range(0, random.randint(0, 14)):
            ret += '='
        ret += 'D'
        if random.randint(0, 1) == 0:
            ret += random.choice([" ((0)) ", " (.)(.)"])
        phenny.say(ret)
f_dick.commands = ['$8\=*D']

def f_emota(phenny, input):
    if random.randint(0, 19) == 0:
        phenny.say(random.choice([":-)", ";-)", "x-D", ":-P", "8-)", "8-D", ":-3", ":-D", "ッ"]))
#f_emota.commands = [u'$\:\)|\:d|\:p|\:f|\:o|\:c|\:b|\:\\|\:\/|\;\)|\;d|\;p|\;f|\;o|\;c|\;b|\;\\|\;\/|8\)|b\)|xd']
f_emota.commands = [u'$(?<!\S)[\:\;][-]{0,1}[3eopcdf\/\\\)\|\>\<\(]|(?<!\S)[8b][-]{0,1}\)|(?<!\S)xd']

def f_guten(phenny, input):
    if random.randint(0, 1) == 0:
        phenny.reply("Schwuchtel Arsch in der Nähe!!!")
f_guten.commands = [u'$guten tag']

def f_hehe(phenny, input):
    if random.randint(0, 19) == 0:
        let = random.choice(['i', 'o', 'e', 'a'])
        ret = 'H' + let
        for x in range(0, random.randint(0, 14)):
            ret += 'h' + let
        phenny.say(ret)
f_hehe.commands = [u'$hehe|haha|hihi|hoho']

def f_kathrep(phenny, input):
    phenny.say("Kath")
f_kathrep.commands = ['$^kath$']

def f_maciek(phenny, input):
    if input.lower() == "maciek":
        phenny.say("Maćku")
f_maciek.commands = ['$maciek']

def f_rucha(phenny, input):
    if random.randint(0, 19) == 0 or input.nick == "seeker2":
        phenny.reply("ruchasz psa jak sra")
f_rucha.commands = ['$\.\.\.']

def f_wulg(phenny, input):
    if random.randint(0, 19) == 0:
        phenny.reply(random.choice(["może byś tak kurwa nie przeklinał, co?", "bez wulgaryzmów proszę, na ten kanał zaglądają dzieci", "ostrożniej z językiem, to kanał PG13", "czy mam ci język uciąć?", "przestań przeklinać pedale bo cię stąd wypierdolę dyscyplinarnie", "pambuk płacze jak przeklinasz", "mów do mnie brzydko", "Kath bączy jak przeklinasz"]))
f_wulg.commands = ['$kurw|chuj|pierdol|pierdal|jeb']

def f_nargoghtime(phenny, input):
    if random.randint(0, 19) == 0:
        phenny.reply("żaden kurwa nargogh time")
f_nargoghtime.commands = ['$nargogh time']

def f_opti(phenny, input):
    if random.randint(0, 19) == 0:
        phenny.reply(random.choice(["uprzejmie proszę o nieużywanie słowa \"opti\" na terenie #nfaircc7. Dziękuję."]))
f_opti.commands = ['$opti']

def f_pedal(phenny, input):
    if random.randint(0, 19) == 0:
        phenny.reply(random.choice(["sam jesteś pedałem", "sam jesteś gejem", "lubię homosiów, chodź przytul mnie"]))
f_pedal.commands = [u'$pedal|pedał|gej|gay']

def f_witam(phenny, input):
    if random.randint(0, 1) == 0:
        phenny.say(random.choice(["witam na kanale i życzę miłej zabawy", "cześć, kopę lat", "siemanko witam na moim kanale", "witam witam również", "no elo", "salam alejkum", "привет", "dzińdybry", "pozdrawiam, " + phenny.nick.split('_')[0] + " " + random.choice(["Gambal", "Handzlik"]), "feedlysiemka " + input.nick.lower() + "ox"]))
f_witam.commands = [u'$witam|cześć|czesc|siema|szalom|joł|shalom|dzindybry|dzie(n|ń) dobry|siemka']

# -------------------------------------
# funkcje używające seed
# -------------------------------------

def f_czy(phenny, input):
    if input != ".czy":
        seed = ''.join(ch for ch, _ in itertools.groupby(''.join(sorted(re.sub("[^a-z0-9]", "", replace_all(input, {u'Ą':'A', u'Ę':'E', u'Ó':'O', u'Ś':'S', u'Ł':'L', u'Ż':'Z', u'Ź':'Z', u'Ć':'C', u'Ń':'N', u'ą':'a', u'ę':'e', u'ó':'o', u'ś':'s', u'ł':'l', u'ż':'z', u'ź':'z', u'ć':'c', u'ń':'n'}).lower())[3:]))))
        random.seed(seed)
    nick = random.choice(["Turambara", "yojca", "Lorda Nargogha", "Rankina", "Kathai", "Behemorta", "orgieła", "Stillborna", "Metalusa", "kicka", "podbiela", "Sedinusa", "Hakkena", "Tebega", "Sermacieja", "t3trisa", "optiego", "Hrabulę", "FaceDancera", "Holiego.Deatha", "lghosta", "POLIPa", "mateusza(stefana)", "Xysia", "Germanottę", "Berlina", "8azyliszka", "Seekera", "Murezora", "RIPa", "Aidena", "Trepliev", "Accouna"])
    phenny.reply(random.choice(["tak", "nie", "nie wiem", "być może", "na pewno", "to mało prawdopodobne", "nie sądzę", "jeszcze się pytasz?", "tak (żartuję, hehe)", "tak", "nie", "tak (no homo)", "zaiste", "no chyba cię pambuk opuścił", "raczej nie", "jeszcze nie", "teraz już tak", "może kiedyś", "nie wiem, spytaj " + nick, "tak jest, panie kapitanie", "panie januszu NIE"]))
f_czy.commands = ['czy']

def f_ile(phenny, input):
    if input != ".ile":
        seed = ''.join(ch for ch, _ in itertools.groupby(''.join(sorted(re.sub("[^a-z0-9]", "", replace_all(input, {u'Ą':'A', u'Ę':'E', u'Ó':'O', u'Ś':'S', u'Ł':'L', u'Ż':'Z', u'Ź':'Z', u'Ć':'C', u'Ń':'N', u'ą':'a', u'ę':'e', u'ó':'o', u'ś':'s', u'ł':'l', u'ż':'z', u'ź':'z', u'ć':'c', u'ń':'n'}).lower())[3:]))))
        random.seed(seed)
    zeros = random.randint(1,4)
    phenny.reply(str(random.randint(pow(10, zeros-1), pow(10, zeros))))
f_ile.commands = ['ile']

def f_nickrand(phenny, input):
    phrase = replace_all(input.group(2), {u'Ą':'A', u'Ę':'E', u'Ó':'O', u'Ś':'S', u'Ł':'L', u'Ż':'Z', u'Ź':'Z', u'Ć':'C', u'Ń':'N', u'ą':'a', u'ę':'e', u'ó':'o', u'ś':'s', u'ł':'l', u'ż':'z', u'ź':'z', u'ć':'c', u'ń':'n'})
    uniq = ''.join(ch for ch, _ in itertools.groupby(''.join(sorted(re.sub("[^a-z]", "", phrase.lower())))))
    uniqV = ''
    uniqC = ''
    uniqN = '0123456789'
    
    for letter in uniq:
        if letter in "eyuioa":
            uniqV += letter
        else:
            uniqC += letter
    
    uniqVS = ''.join(random.sample(uniqV,len(uniqV)))
    uniqCS = ''.join(random.sample(uniqC,len(uniqC)))
    uniqNS = ''.join(random.sample(uniqN,len(uniqN)))
    
    mask = ''
    for letter in phrase:
        if letter.islower():
            mask += 'a'
        elif letter.isupper():
            mask += 'A'
        else:
            mask += '#'
    
    ret = ''
    for letter in phrase.lower():
        if uniqV.find(letter) > -1:
            ret += uniqVS[uniqV.find(letter)]
        elif uniqC.find(letter) > -1:
            ret += uniqCS[uniqC.find(letter)]
        elif uniqN.find(letter) > -1:
            ret += uniqNS[uniqN.find(letter)]
        else:
            ret += letter
    
    ret = list(ret)
    for i in range(0, len(ret)):
        if mask[i].islower():
            ret[i] = ret[i].lower()
        elif mask[i].isupper():
            ret[i] = ret[i].upper()
    
    phenny.say(''.join(ret))
    #phenny.say(phrase + " : " + ret + " : " + uniq + " : " + uniqV + " -> " + uniqVS + " : " + uniqC + " -> " + uniqCS)
f_nickrand.commands = ['rn']

def f_ocen(phenny, input):
    if input != ".ocen":
        seed = ''.join(ch for ch, _ in itertools.groupby(''.join(sorted(re.sub("[^a-z0-9]", "", replace_all(input, {u'Ą':'A', u'Ę':'E', u'Ó':'O', u'Ś':'S', u'Ł':'L', u'Ż':'Z', u'Ź':'Z', u'Ć':'C', u'Ń':'N', u'ą':'a', u'ę':'e', u'ó':'o', u'ś':'s', u'ł':'l', u'ż':'z', u'ź':'z', u'ć':'c', u'ń':'n'}).lower())[3:]))))
        random.seed(seed)
    ocena = random.randint(0, 10)
    doda = ""
    znak = ""
    if ocena < 10 and ocena > 0:
        doda = random.choice(["", ",5", "-", "+"])
    if ocena > 7:
        znak = random.choice(["", "+ znak jakości CD-Action", "- Berlin poleca", ""])
    phenny.reply(str(ocena) + doda + "/10 " + znak)
f_ocen.commands = ['ocen']

def f_test(phenny, input):
    phenny.say(''.join(ch for ch, _ in itertools.groupby(''.join(sorted(re.sub("[^a-z]", "", replace_all(input, {u'Ą':'A', u'Ę':'E', u'Ó':'O', u'Ś':'S', u'Ł':'L', u'Ż':'Z', u'Ź':'Z', u'Ć':'C', u'Ń':'N', u'ą':'a', u'ę':'e', u'ó':'o', u'ś':'s', u'ł':'l', u'ż':'z', u'ź':'z', u'ć':'c', u'ń':'n'}).lower())[3:])))))
f_test.commands = ['test']

# -------------------------------------
# przedrzeźnianie
# -------------------------------------

def f_baki(phenny, input):
    out = ""
    for i in range(1, random.randint(2, 6)):
        if i == 1:
            out += "P"
        else:
            out += " p"
        
        for j in range(1, random.randint(2, 10)):
            out += "f"
        out += "rt"
    phenny.say(out)
f_baki.commands = ['baki|pfrt']

def f_behe(phenny, input):
    ret = 'Be'
    for x in range(0, random.randint(1, 15)):
        ret += 'he'
    phenny.say(ret+'mort')
f_behe.commands = ['behe']

def f_hakken(phenny, input):
    ret = 'Ha'
    for x in range(0, random.randint(0, 14)):
        ret += 'ha'
    phenny.say(ret+'kken')
f_hakken.commands = ['haken|hakken']

def f_kath(phenny, input):
    ret = 'Ka'
    for x in range(0, random.randint(0, 14)):
        ret += 'ka'
    ret += "thai_Na"
    for x in range(0, random.randint(0, 14)):
        ret += 'na'
    phenny.say(ret+'njika')
f_kath.commands = ['kath']

def f_kicek(phenny, input):
    phenny.say(random.choice(["kicek", "kiceg"]) + " mały " + random.choice(["bicek", "dicek"]))
f_kicek.commands = ['kicek|kiceg']

def f_nargog(phenny, input):
    ret = 'Na'
    for x in range(0, random.randint(0, 14)):
        ret += 'na'
    phenny.say(ret+'rgogh')
f_nargog.commands = ['nargog|nargogh']

def f_polip(phenny, input):
    ret = 'POLI'
    for x in range(0, random.randint(0, 14)):
        ret += 'POLI'
    phenny.say(ret + random.choice(["P", "POLIK"]))
f_polip.commands = ['polip']

def f_rane(phenny, input):
    ret = "Ra"
    for x in range(0, 14):
        if random.randint(0, 1) == 1:
            ret += 'ra'
        else:
            ret += "ne"
    phenny.say(ret)
f_rane.commands = ['rane']

def f_teb(phenny, input):
    ret = 'Teb'
    for x in range(0, random.randint(0, 14)):
        ret += 'eb'
    phenny.say(ret+'eg')
f_teb.commands = ['teb']

def f_yojc(phenny, input):
    count = random.randint(0, 14)
    flag = random.randint(0, 1)
    
    if count < 2:
        where = 0
    else:
        where = random.randint(1, count-1)
    
    ret = "yo"
    
    for x in range(0, count):
        ret += 'yo'
        if flag == 1 and x == where:
            ret += "motherfucker"
    
    phenny.say(ret+'jc')
f_yojc.commands = ['yojc']

# -------------------------------------
# inne
# -------------------------------------

def f_taknie(phenny, input):
     phenny.say(random.choice(["tak", "nie"]))
f_taknie.commands = ['taknie']

def f_abcd(phenny, input):
    phenny.say(random.choice("abcd"))
f_abcd.commands = ['abcd']

def f_bazinga(phenny, input):
     phenny.say(random.choice(["BAZINGA!", "BIGANPIZA!", "ZIMBABWE!"]))
f_bazinga.commands = ['bazinga|biganpiza|zimbabwe']

def f_boruc(phenny, input):
     phenny.say("brawo " + boruc)
f_boruc.commands = ['boruc|brawo']

def f_co(phenny, input):
     phenny.reply("gówno")
f_co.commands = ['co']

def f_licz(phenny, input):
    if input.group(2) != None:
        ile = int(input.group(2).rstrip());
        if ile > 5:
            phenny.say("do tylu nie umiem")
        else:
            for i in reversed(range(ile+1)):
                phenny.say(str(i))
                time.sleep(1)
    else:
        phenny.say("sam se licz")
f_licz.commands = ['licz']

def f_pac(phenny, input):
    if input.startswith(".pac"):
        if input.group(2) != None:
            if input.group(2).rstrip().lower() == phenny.nick.lower() or input.group(2).rstrip().lower() == "himself" or input.group(2).rstrip().lower() == "myself":
                who = u"nij się w swój pusty łeb "
            else:
                who = u" " + input.group(2).rstrip() + u" 4,13♥0,13PAC4,13♥ "
        else:
            who = u" "
    
    phenny.say(u"0,13 4,13♥0,13PAC4,13♥0,13" + who);
f_pac.commands = ['pac']

def f_piwo(phenny, input):
    yojc = ""
    who = "Kath"
    
    if input.group(2) != None:
        if re.search("(y|j){1}o(y|j){1}(e.|c|o)", input.group(2).rstrip()):
            yojc = " dla yojca"
            who = input.nick
        else:
            who = input.group(2).rstrip()
    
    actions = ["skocz", u"idź", u"przeleć się"]
    places = ["osiedlowego", "monopolowego", u"lodówki", "barku", "sklepu", "Biedronki", "baru"]
    beers = ["zimnego Lecha", "Ciechana miodowego", u"Złotego Bażanta", "Heinekena", "Carlsberga", "Pilsnera Urquella", "Budweisera", "Rolling Rock"]
    
    phenny.say(who + ", " + random.choice(actions) + " do " + random.choice(places) + " po " + random.choice(beers) + yojc);
f_piwo.commands = ['piwo|barman']

def f_pfrt(phenny, input):
    phenny.say("rt")
f_pfrt.commands = ['$^pf+$']

def f_sayto(phenny, input):
    phenny.msg("#nfaircc7", input.group(2).rstrip())
f_sayto.commands = ['say']

def f_sing(phenny, input):
    phenny.say("Nie umiem śpiewać.");
f_sing.commands = ['sing']

def f_tebeg(phenny, input):
    phenny.say("T E B E G")
    phenny.say("E")
    phenny.say("B")
    phenny.say("E")
    phenny.say("G")
f_tebeg.commands = ['tebeg']

def f_quit(phenny, input):
    phenny.say(act("has quit (" + random.choice(["Ping timeout", "idese", "Connection reset by peer", ".net.split", "opti to twoj stary"]) + ")"))
f_quit.commands = ['quit']