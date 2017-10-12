﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;

namespace TagsCloudContainer
{
    class Program
    {
        private static readonly Container Container;
        private static string fileName = "words.txt";
        private static Point _center;

        static Program()
        {
            string[] boringWords = new[] { "Аврора", "Агата", "Александрина", "Алира", "Альберта", "Авигея" };

            Container = new Container();
            Container.Register<IFileParser>(() => new TxtFileParser(fileName));
            Container.Register<BoringWordsFormater>(() => new BoringWordsFormater(boringWords));
            Container.RegisterCollection<IWordFormater>(new[] { typeof(BoringWordsFormater), typeof(LowerCaseFormater) });
            Container.Register<IWordPreprocessor, SimpleWordPreprocessor>();
            Container.Register<ITagsData, TagsData>();
            Container.Register<ICircularCloudLayouter>(() => new CircularCloudLayouter(_center), Lifestyle.Singleton);
            Container.Register<TagsCloudContainer>(Lifestyle.Singleton);
            Container.Verify();
        }

        static void Main(string[] args)
        {
            using (Container)
            {
                var tcc = Container.GetInstance<TagsCloudContainer>();
            }


        }
    }
}
